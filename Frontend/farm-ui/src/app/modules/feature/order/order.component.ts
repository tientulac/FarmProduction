import { Component, Inject, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzPlacementType } from 'ng-zorro-antd/dropdown';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { OrderEntity, OrderEntitySearch } from 'src/app/entities/Order.Entity';
import { OrderItemEntity, OrderItemEntitySearch } from 'src/app/entities/OrderItem.Entity';
import { ProductAttributeEntity } from 'src/app/entities/ProductAttribute.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent extends BaseComponent<OrderEntity> {

  listType: any = [
    { id: 0, name: 'Online' },
    { id: 1, name: 'Trực tiếp' },
  ];


  listPaymentType: any = [
    { id: 0, name: 'Tiền mặt' },
    { id: 1, name: 'Chuyển khoản' },
  ];

  orderItemEntity = new OrderItemEntity();
  orderItemEntitySearch = new OrderItemEntitySearch();
  orderItemEntities!: OrderItemEntity[];
  productAttributeEntity = new ProductAttributeEntity();

  constructor(
    public orderItemService: BaseService<OrderItemEntity>,
    public productAttributeService: BaseService<ProductAttributeEntity>,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<OrderEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new OrderEntity();
    this.EntitySearch = new OrderEntitySearch();
    this.Entities = new Array<OrderEntity>();;
    this.URL = 'order';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Hóa đơn');
    this.field_Validation = {
      name: false
    };

    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;
    this.GROUP_BUTTON.EXCEL = true;

    this.listStatus = [
      { id: 0, name: 'Đang chờ duyệt' },
      { id: 1, name: 'Đang vận chuyển' },
      { id: 2, name: 'Đã duyệt' },
      { id: 3, name: 'Đã hủy' },
      { id: 4, name: 'Giao thành công' },
    ];

    this.getList();
    this.getListCity();
  }


  renderName(list: any, value: any) {
    return list.filter((x: any) => x.id == value)[0].name ?? '';
  }

  override getList() {
    this.baseService.getByRequest(this.URL, this.EntitySearch).subscribe(
      (res) => {
        this.Entities = res.data;
        if (this.Entities.length > 0) {
          this.Entities.forEach((order: OrderEntity) => {
            this.baseService.getListCity().subscribe(
              (res) => {
                let city = res.data.filter((x: any) => x.ProvinceID == parseInt(order.provinceToId ?? ''))[0] ?? null;
                if (city) {
                  order.address = `${city.ProvinceName}`;
                  this.baseService.getListDistrict({ province_id: parseInt(order.provinceToId ?? '') }).subscribe(
                    (res) => {
                      let district = res.data.filter((x: any) => x.DistrictID == parseInt(order.districtToId ?? ''))[0] ?? null;
                      if (district) {
                        order.address += `- ${district.DistrictName}`;
                        this.baseService.getListWard({ district_id: parseInt(order.districtToId ?? '') }).subscribe(
                          (res) => {
                            let ward = res.data.filter((x: any) => x.WardCode === order.wardToId)[0] ?? null;
                            if (ward) {
                              order.address += `- ${ward.WardName}`;
                            }
                          }
                        );
                      }
                    }
                  );
                }
              }
            );
          })
        }
      }
    );
  }

  openModal(type: any, data: OrderEntity) {
    this.isFilter = false;
    this.Entity = data;
    this.productAttributeEntity = new ProductAttributeEntity();
    if (type === 'EDIT') {
      this.isInsert = true;
    }
    if (type === 'DETAIL') {
      this.isEdit = false;
      this.orderItemEntitySearch = new OrderItemEntitySearch();
      this.orderItemEntitySearch.orderId = data.id;
      this.isDisplayDetail = true;
      this.getOrderItem();
    }
  }

  async onSubmit(): Promise<boolean> {
    this.onSubmitting = true;
    this.save();
    return true;
  }

  override handleCancel(): void {
    this.getList();
    this.isInsert = false;
    this.isDisplayDelete = false;
    this.isDisplayDetail = false;
  }

  deleteAttribute(id: any) {
    this.orderItemService.delete(`orderItem/${id}`).subscribe(
      (res: ReponseAPI<any>) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.getOrderItem();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  getOrderItem() {
    this.orderItemService.getAll(`OrderItem?orderId=${this.orderItemEntitySearch.orderId}`).subscribe(
      (res) => {
        this.orderItemEntities = res.data;
      }
    );
  }

  addOrderItem() {
    if (!this.productAttributeEntity.code && !this.productAttributeEntity.productName && !this.productAttributeEntity.color) {
      this.toastr.warning('Bạn cần nhập một trong các thuộc tính cần thiết');
    }
    else {
      this.productAttributeEntity.orderId = this.orderItemEntitySearch.orderId;
      this.productAttributeService.save('OrderItem/InsertOrderItem', this.productAttributeEntity).subscribe(
        (res) => {
          if (res.code == '200') {
            this.toastr.success('Thành công');
            this.getOrderItem();
          }
          else {
            this.toastr.warning('Thất bại');
          }
        }
      );
    }
  }

  updateOderItem(orderItem: OrderItemEntity) {
    this.orderItemService.save('OrderItem/UpdateOrderItem', orderItem).subscribe(
      (res) => {
        if (res.code == '200') {
          this.toastr.success('Thành công');
          this.getOrderItem();
        }
        else {
          this.toastr.warning('Thất bại');
        }
      }
    );
  }
}

import { Component, Inject } from '@angular/core';
import { UserAccountEntity } from '../entities/UserAccount.Entity';
import { BaseComponent } from '../_core/base/base.component';
import { BaseService } from '../services/base.service';
import { Router } from '@angular/router';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { AppInjector } from '../app.module';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Title } from '@angular/platform-browser';
import { UploadImageService } from '../services/upload-image.service';
import { ToastrService } from 'ngx-toastr';
import { OrderItemEntity, OrderItemEntitySearch } from '../entities/OrderItem.Entity';
import { ProductAttributeEntity } from '../entities/ProductAttribute.Entity';
import { OrderEntity } from '../entities/Order.Entity';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent extends BaseComponent<UserAccountEntity>{

  userOrder = new OrderEntity();
  orderItemEntitySearch = new OrderItemEntitySearch();
  orderItemEntities!: OrderItemEntity[];
  listOrder: any;
  listPaymentType: any = [
    { id: 0, name: 'Tiền mặt' },
    { id: 1, name: 'Chuyển khoản' },
  ];
  listType: any = [
    { id: 0, name: 'Online' },
    { id: 1, name: 'Trực tiếp' },
  ];
  constructor(
    public accountService: BaseService<UserAccountEntity>,
    public orderService: BaseService<OrderEntity>,
    public orderItemService: BaseService<OrderItemEntity>,
    private router: Router,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<UserAccountEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new UserAccountEntity();
    this.EntitySearch = new UserAccountEntity();
    this.Entities = new Array<UserAccountEntity>();
    this.URL = 'userSite/category';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Thông tin cá nhân');
    this.listStatus = [
      { id: 0, name: 'Đang chờ duyệt' },
      { id: 1, name: 'Đang vận chuyển' },
      { id: 2, name: 'Đã duyệt' },
      { id: 3, name: 'Đã hủy' },
      { id: 4, name: 'Giao thành công' },
    ];
    this.checkLogin();
    this.getCountCart();
    this.getListCity();
    this.getListOrder();
  }

  getListOrder() {
    let req = new OrderEntity();
    req.userAccountId = this.userInfo.id;
    this.orderService.getByRequest('usersite/order', req).subscribe(
      (res) => {
        this.listOrder = res.data;
        if (this.listOrder.length > 0) {
          this.listOrder.forEach((order: OrderEntity) => {
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

  updateInfo() {
    this.baseService.save('userAccount/update-info', this.userInfo).subscribe(
      (res) => {
        if (res.code == '200') {
          this.toastr.success('Thành công');
          localStorage.setItem('UserInfo', JSON.stringify(this.userInfo));
        }
        else {
          this.toastr.warning(res.message ?? '');
        }
      }
    );
  }

  changePass() {
    if (this.isChangePass) {
      if (!this.Entity?.oldHashPassword || !this.Entity?.newHashPassword) {
        this.toastr.warning('Mật khẩu không được bỏ trống');
        return;
      }
      if (this.Entity?.oldHashPassword != this.Entity?.newHashPassword) {
        this.toastr.warning('Xác nhận mật khẩu chưa đúng');
        return;
      }
      this.baseService.save('userAccount/change-pass', this.Entity).subscribe(
        (res) => {
          if (res.code == '200') {
            this.toastr.success('Thành công. Hệ thống sẽ yêu cầu đăng nhập lại sau 3s');
          }
          else {
            this.toastr.warning(res.message ?? '');
          }
        }
      );
    }
  }

  createOrder() {
    this.userOrder.userAccountId = this.userInfo.id;
    this.userOrder.total = this.totalPrice;
    this.userOrder.paymentShip = this.feeShip;
    this.userOrder.status = 0;
    this.userOrder.code = `HD_${Math.random().toString(36).substring(2, 7)}`;
    this.userOrder.listItem = this.listCartItem;
    this.userOrder.type = 0;
    this.orderService.save('order', this.userOrder).subscribe(
      (res) => {
        if (res.code == '200') {
          this.toastr.success('Thành công');
          this.handleCancel();
          localStorage.setItem('listCartItem', '[]');
          this.getCountCart();
        }
        else {
          this.toastr.warning('Thất bại');
        }
      }
    );
  }

  updateOderItem(p: ProductAttributeEntity) {
    if (p.amountBought - (p.amount ?? 0) > 0) {
      this.toastr.warning('Số lượng sản phẩm không được lớn hơn số lượng trong kho');
    }
    else {
      var cItem = this.listCartItem.filter((x: ProductAttributeEntity) => x.id == p.id)[0];
      if (cItem) {
        this.listCartItem.forEach((x: ProductAttributeEntity) => {
          if (x.id == cItem.id) {
            x.amountBought = p.amountBought;
          }
        });
      }
      this.setCartItem();
      this.toastr.success('Thành công');
      this.isEdit = false;
    }
  }

  deleteOrderItem(p: ProductAttributeEntity) {
    this.listCartItem = this.listCartItem.filter((x: ProductAttributeEntity) => x.id != p.id);
    this.setCartItem();
    this.getCountCart();
    this.toastr.success('Xóa sản phẩm thành công');
  }

  openConfirmModal() {
    this.isInsert = true;
    this.totalPrice = 0;
    this.userOrder = new OrderEntity();
    if (this.listCartItem?.length > 0) {
      this.listCartItem.forEach((x: ProductAttributeEntity) => {
        this.totalPrice += x.amountBought * (x.price ?? 0);
      });
    }
  }

  renderName(list: any, value: any) {
    return list.filter((x: any) => x.id == value)[0].name ?? '';
  }

  openModal(type: any, data: OrderEntity) {
    if (type === 'DETAIL') {
      this.orderItemEntitySearch = new OrderItemEntitySearch();
      this.orderItemEntitySearch.orderId = data.id;
      this.isDisplayDetail = true;
      this.getOrderItem();
    }
  }

  getOrderItem() {
    this.orderItemService.getAll(`OrderItem?orderId=${this.orderItemEntitySearch.orderId}`).subscribe(
      (res) => {
        this.orderItemEntities = res.data;
      }
    );
  }

  cancleOrder() {
    this.orderService.getAll(`UserSite/cancle-order?Id=${this.id_record}`).subscribe(
      (res) => {
        if (res.code == '200') {
          this.toastr.success('Hủy thành công đơn hàng');
          this.handleCancel();
          this.getListOrder();
        }
        else {
          this.toastr.warning(res.message ?? '');
        }
      }
    );
  }
}

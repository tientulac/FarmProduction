import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { OrderEntity, OrderEntitySearch } from 'src/app/entities/Order.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent extends BaseComponent<OrderEntity>{

  listType: any = [
    { id: 0, name: 'Online' },
    { id: 1, name: 'Trực tiếp' },
  ];


  listPaymentType: any = [
    { id: 0, name: 'Tiền mặt' },
    { id: 1, name: 'Chuyển khoản' },
  ];

  constructor(
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
}

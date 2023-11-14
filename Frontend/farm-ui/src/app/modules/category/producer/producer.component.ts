import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { ProducerEntity, ProducerEntitySearch } from 'src/app/entities/Producer.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-producer',
  templateUrl: './producer.component.html',
  styleUrls: ['./producer.component.scss']
})
export class ProducerComponent extends BaseComponent<ProducerEntity>{

  listTypeModel = [
    { name: 'Thương mại', id: 0 },
    { name: 'Nhập khẩu', id: 1 },
    { name: 'Xuất khẩu', id: 2 },
  ];

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<ProducerEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new ProducerEntity();
    this.EntitySearch = new ProducerEntitySearch();
    this.Entities = new Array<ProducerEntity>();;
    this.URL = 'producer';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Nhà sản xuất');
    this.field_Validation = {
      name: false,
      code: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;

    this.getList();
  }

  async onSubmit(): Promise<boolean> {
    this.onSubmitting = true;
    if (!this.isSubmit) {
      this.toastr.warning('Dữ liệu nhập chưa hợp lệ');
      return false;
    }
    this.save();
    return true;
  }

  openModal(type: any, data: ProducerEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new ProducerEntity();
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  renderTypeModel(typeModel: any) {
    return this.listTypeModel.filter((x: any) => x.id == typeModel)[0].name ?? '';
  }
}

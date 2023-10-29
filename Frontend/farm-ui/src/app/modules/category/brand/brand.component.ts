import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { BrandEntity, BrandEntitySearch } from 'src/app/entities/Brand.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent extends BaseComponent<BrandEntity> {

  listName: any;

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<BrandEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
    );
    this.Entity = new BrandEntity();
    this.EntitySearch = new BrandEntitySearch();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'brand';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Thương hiệu');
    this.field_Validation = {
      name: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;

    this.listName = [
      { id: 1, name: 'TienNN' },
      { id: 2, name: 'TienNN2' },
    ];
    this.getList();
  }

  async onSubmit(): Promise<boolean> {
    this.onSubmitting = true;
    if (!this.isSubmit) {
      alert('Dữ liệu nhập chưa hợp lệ');
      return false;
    }
    this.save();
    return true;
  }

  openModal(type: any, data: BrandEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new BrandEntity();
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  handleUpload = (item: any) => {
    const formData = new FormData();
    formData.append(item.name, item.file as any, this.uploadFileName);
    this.uploadImageService.upload(formData).subscribe(
      (res: ReponseAPI<File>) => {
        item.onSuccess(item.file);
        if (res.code == "200") {
          this.Entity!.image = res.message;
        }
        else {
          alert(res.message);
        }
      }
    );
  };

  beforeUpload = (file: NzUploadFile): boolean => {
    this.uploadFileName = `brand_${this.Entity?.code}.jpg`;
    return true;
  };
}

import { Component, Inject, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { CategoryEntity, CategoryEntitySearch } from 'src/app/entities/Category.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent extends BaseComponent<CategoryEntity> {

  categories!: CategoryEntitySearch[];

  constructor(
    public categoryService: BaseService<CategoryEntity>,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<CategoryEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new CategoryEntity();
    this.EntitySearch = new CategoryEntitySearch();
    this.Entities = new Array<CategoryEntity>();;
    this.URL = 'category';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Danh mục');
    this.field_Validation = {
      name: false
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
      this.toastr.warning('Dữ liệu nhập vào chưa hợp lệ !');
      return false;
    }
    this.save();
    return true;
  }

  openModal(type: any, data: CategoryEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new CategoryEntity();
    this.getListParentCategory();
    this.Entity.code = Math.random().toString(36).substring(2, 7);
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  getListParentCategory() {
    this.categoryService.getByRequest('category', {}).subscribe(
      (res: ReponseAPI<any>) => {
        this.categories = res.data;
      }
    );
  }
}

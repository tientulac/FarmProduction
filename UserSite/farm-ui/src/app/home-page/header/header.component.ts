import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { CategoryEntity, CategoryEntitySearch } from 'src/app/entities/Category.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseComponent<CategoryEntity>{

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
    this.URL = 'userSite/category';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Danh mục');

    this.getList();
  }
}

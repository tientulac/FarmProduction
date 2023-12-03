import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
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
    private router: Router,
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
    this.checkLogin();
    this.getCountCart();
    this.getList();
  }

  activeNavbar(name: any) {
    if (name == this.router.url) {
      return 'nav-item nav-link active';
    }
    return 'nav-item nav-link';
  }

  logout() {
    localStorage.clear();
    this.checkLogin();
    window.location.href = '/';
  }
}

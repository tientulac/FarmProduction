import { Component, Inject } from '@angular/core';
import { BaseComponent } from '../_core/base/base.component';
import { ProductAttributeEntity, ProductAttributeSearchEntity } from '../entities/ProductAttribute.Entity';
import { BaseService } from '../services/base.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { AppInjector } from '../app.module';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Title } from '@angular/platform-browser';
import { UploadImageService } from '../services/upload-image.service';
import { ToastrService } from 'ngx-toastr';
import { BrandEntity, BrandEntitySearch } from '../entities/Brand.Entity';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent extends BaseComponent<BrandEntity>{

  constructor(
    public brandService: BaseService<BrandEntity>,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<BrandEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new BrandEntity();
    this.EntitySearch = new BrandEntitySearch();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'userSite/product/category/:id';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Danh sách sản phẩm');
    alert(this.route.snapshot.paramMap.get('id'))
    this.getList();
  }
}

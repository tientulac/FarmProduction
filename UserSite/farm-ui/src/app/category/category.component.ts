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
import { ProductEntity, ProductEntitySearch } from '../entities/Product.Entity';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent extends BaseComponent<ProductEntity>{

  id_category: any;
  product_search = new ProductEntitySearch();

  constructor(
    public brandService: BaseService<BrandEntity>,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<ProductEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new ProductEntity();
    this.Entities = new Array<ProductEntity>();;
    this.URL = 'userSite/product';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Danh sách sản phẩm');
    this.id_category = this.route.snapshot.paramMap.get('id');
    this.getListProduct();
  }

  getListProduct() {
    this.product_search.categoryId = this.id_category;
    this.product_search.searchString = this.keyword;
    this.baseService.getByRequest(this.URL, this.product_search).subscribe(
      (res) => {
        this.Entities = res.data;
      }
    );
  }
}

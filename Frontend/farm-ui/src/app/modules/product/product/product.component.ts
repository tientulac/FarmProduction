import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { BrandEntity, BrandEntitySearch } from 'src/app/entities/Brand.Entity';
import { CategoryEntity, CategoryEntitySearch } from 'src/app/entities/Category.Entity';
import { ProductEntity, ProductEntitySearch } from 'src/app/entities/Product.Entity';
import { ProductDescriptionEntity } from 'src/app/entities/ProductDescription.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent extends BaseComponent<ProductEntity> {

  categories!: CategoryEntitySearch[];
  brands!: BrandEntitySearch[];
  public Editor = ClassicEditor;
  EntityDescription = new ProductDescriptionEntity();

  constructor(
    public categoryService: BaseService<CategoryEntity>,
    public brandService: BaseService<BrandEntity>,
    public productDescriptionService: BaseService<ProductDescriptionEntity>,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<ProductEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
    );
    this.Entity = new ProductEntity();
    this.EntitySearch = new ProductEntitySearch();
    this.Entities = new Array<ProductEntity>();;
    this.URL = 'product';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Sản phẩm');
    this.field_Validation = {
      name: false,
      categoryId: false,
      brandId: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;

    this.getList();
  }

  ngOnInit() {
    this.getListFilter();
  }

  getListFilter() {
    this.categoryService.getAll('category', {}).subscribe(
      (res: ReponseAPI<any>) => {
        this.categories = res.data;
      }
    );
    this.brandService.getAll('brand', {}).subscribe(
      (res: ReponseAPI<any>) => {
        this.brands = res.data;
      }
    );
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

  openModal(type: any, data: ProductEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new ProductEntity();
    this.Entity.code = Math.random().toString(36).substring(2, 7);
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  addProductDescription() {
    this.EntityDescription.productId = this.id_record;
    this.EntityDescription.description = this.Entity.description;
    this.productDescriptionService.save('ProductDescription', this.EntityDescription).subscribe(
      (res) => {
        if (res.code == "200") {
          alert("Thành công !");
          this.EntityDescription = res.data;
          this.handleCancel();
        }
        else {
          alert(res.messageEX);
        }
      }
    );
  }
}

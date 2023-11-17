import { Component, Inject, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { BrandEntity, BrandEntitySearch } from 'src/app/entities/Brand.Entity';
import { CategoryEntity, CategoryEntitySearch } from 'src/app/entities/Category.Entity';
import { ProductEntity, ProductEntitySearch } from 'src/app/entities/Product.Entity';
import { ProductAttributeEntity, ProductAttributeSearchEntity } from 'src/app/entities/ProductAttribute.Entity';
import { ProductDescriptionEntity } from 'src/app/entities/ProductDescription.Entity';
import { ProductImageEntity } from 'src/app/entities/ProductImage.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent extends BaseComponent<ProductEntity> implements OnInit {

  categories!: CategoryEntitySearch[];
  brands!: BrandEntitySearch[];
  productAttributeEntity!: ProductAttributeEntity;
  productAttributes!: ProductAttributeEntity[];
  productImages!: ProductImageEntity[];
  productImageEntity!: ProductImageEntity;
  productAttributeFilter!: ProductAttributeSearchEntity;
  public Editor = ClassicEditor;
  EntityDescription = new ProductDescriptionEntity();
  productDescriptions!: any;

  constructor(
    public categoryService: BaseService<CategoryEntity>,
    public brandService: BaseService<BrandEntity>,
    public productDescriptionService: BaseService<ProductDescriptionEntity>,
    public productImageService: BaseService<ProductImageEntity>,
    public productAttributeService: BaseService<ProductAttributeEntity>,
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
    this.GROUP_BUTTON.EXCEL = true;

    this.getList();
    this.getListFilter();
  }

  ngOnInit(): void {
  }

  getListFilter() {
    this.categoryService.getAll('category/get-sub-categories').subscribe(
      (res: ReponseAPI<any>) => {
        this.categories = res.data;
      }
    );
    this.brandService.getByRequest('brand', {}).subscribe(
      (res: ReponseAPI<any>) => {
        this.brands = res.data;
      }
    );
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

  openModal(type: any, data: ProductEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new ProductEntity();
    this.Entity.code = Math.random().toString(36).substring(2, 7);
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  openAttributeModal(data: ProductEntity) {
    this.isDisplayAttribute = true;
    this.listFileUpload = [];
    this.productAttributeFilter = new ProductAttributeSearchEntity();
    this.productAttributeEntity = new ProductAttributeEntity();
    this.productImageEntity = new ProductImageEntity();
    this.id_record = data.id;
    this.getListAttribute();
    this.getListImage();
  }

  addProductDescription() {
    this.EntityDescription.productId = this.id_record;
    this.EntityDescription.description = this.Entity.description;
    this.productDescriptionService.save('ProductDescription', this.EntityDescription).subscribe(
      (res) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.EntityDescription = res.data;
          this.handleCancel();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  getProductDescriptions(p_id: any) {
    this.productDescriptionService.getById(`ProductDescription?ProductId=${p_id}`).subscribe(
      (res) => {
        this.productDescriptions = res.data;
      }
    );
  }

  deleteProductDescription(desc_id: any) {
    this.productDescriptionService.delete(`ProductDescription/${desc_id}`).subscribe(
      (res) => {
        this.getProductDescriptions(this.id_record);
      }
    );
  }

  getListAttribute() {
    this.productAttributeService.getByRequest('ProductAttribute', this.productAttributeFilter).subscribe(
      (res: ReponseAPI<any>) => {
        this.productAttributes = res.data;
      }
    );
  }

  getListImage() {
    this.productImageService.getAll(`ProductImage?ProductId=${this.id_record}`).subscribe(
      (res: ReponseAPI<any>) => {
        this.productImages = res.data;
      }
    );
  }

  saveAttribute() {
    this.productAttributeEntity.productId = this.id_record;
    this.productAttributeService.save('ProductAttribute', this.productAttributeEntity).subscribe(
      (res: ReponseAPI<any>) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.getListAttribute();
          this.productAttributeEntity = new ProductAttributeEntity();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  deleteAttribute(id: any) {
    this.productAttributeService.delete(`ProductAttribute/${id}`).subscribe(
      (res: ReponseAPI<any>) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.getListAttribute();
          this.getList();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  saveManyImage() {
    this.productImageService.save('ProductImage/save-many', this.productImageEntity).subscribe(
      (res: ReponseAPI<any>) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.getListImage();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  deleteImage(id: any) {
    this.productAttributeService.delete(`ProductImage/${id}`).subscribe(
      (res: ReponseAPI<any>) => {
        if (res.code == "200") {
          this.toastr.success("Thành công !");
          this.getListImage();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  }

  handleUploadProductImage = (item: any) => {
    const formData = new FormData();
    formData.append(item.name, item.file as any, this.uploadFileName);
    this.uploadImageService.upload(formData).subscribe(
      (res: ReponseAPI<File>) => {
        item.onSuccess(item.file);
        if (res.code == "200") {
          this.Entity.image = res.message;
          this.productImageEntity.productId = this.id_record;
          this.productImageEntity.image = this.uploadFileName;
          this.productImageService.save('ProductImage', this.productImageEntity).subscribe(
            (res: ReponseAPI<any>) => {
              if (res.code == "200") {
                this.toastr.success("Thành công !");
                this.getListImage();
              }
              else {
                this.toastr.warning(res.messageEX?.toString());
              }
            }
          );
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
        }
      }
    );
  };

  editAttibute(data: ProductAttributeEntity) {
    this.id_edit = data.id ?? null;
    this.productAttributeEntity = data;
    this.isEdit = true;
  }

  onChangeDateRange(result: Date[]): void {
    this.productAttributeEntity.manufactureDate = result[0];
    this.productAttributeEntity.expireDate = result[1];
  }
}

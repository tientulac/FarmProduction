import { Component, Inject } from '@angular/core';
import { ProductAttributeEntity, ProductAttributeSearchEntity } from '../entities/ProductAttribute.Entity';
import { BaseComponent } from '../_core/base/base.component';
import { BaseService } from '../services/base.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { AppInjector } from '../app.module';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Title } from '@angular/platform-browser';
import { UploadImageService } from '../services/upload-image.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent extends BaseComponent<ProductAttributeEntity>{

  id_product: any;
  product_search = new ProductAttributeSearchEntity();

  constructor(
    public brandService: BaseService<ProductAttributeEntity>,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<ProductAttributeEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new ProductAttributeEntity();
    this.Entities = new Array<ProductAttributeEntity>();;
    this.URL = 'userSite/product-attribute';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Danh sách sản phẩm');
    this.id_product = this.route.snapshot.paramMap.get('id');
    this.checkLogin();
    this.getListProduct();
    this.getCountCart();
  }

  getListProduct() {
    this.product_search.productId = this.id_product;
    this.baseService.getByRequest(this.URL, this.product_search).subscribe(
      (res) => {
        this.Entities = res.data;
      }
    );
  }

  addToCart(p: ProductAttributeEntity) {
    if (!this.isLogin) {
      this.toastr.warning('Bạn cần đăng nhập để thêm vào giỏ hàng');
      return;
    }
    if (p.expireDate != null && p.expireDate > new Date()) {
      this.toastr.warning('Sản phẩm này đã hết hạn sử dụng');
      return;
    }
    this.addItemToCart(p);
    this.setCartItem();
    this.getCountCart();
    this.toastr.success('Thêm giỏ hàng thành công. Vui lòng vào trang cá nhân để check thông tin giỏ hàng');
  }
}

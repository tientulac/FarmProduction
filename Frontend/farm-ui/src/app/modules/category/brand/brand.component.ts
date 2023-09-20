import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { BrandEntity } from 'src/app/entities/Brand.Entity';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent extends BaseComponent<BrandEntity> {

  constructor(
  ) {
    super(AppInjector.get(BaseService<BrandEntity>));
    this.Entity = new BrandEntity();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'brand';
    this.getList();
  }

  submit() {
    console.log(this.Entity);
  }
}

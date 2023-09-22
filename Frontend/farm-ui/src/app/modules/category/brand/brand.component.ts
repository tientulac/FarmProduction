import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
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

  listName: any;

  constructor(
  ) {
    super(
      AppInjector.get(BaseService<BrandEntity>),
      AppInjector.get(NzModalService)
    );
    this.Entity = new BrandEntity();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'brand';
    this.field_Validation = {
      Code: false,
      Name: false
    };
    this.listName = [
      { id: 1, name: 'TienNN' },
      { id: 2, name: 'TienNN2' },
    ];
    // this.getList();
  }

  onSubmit() {
    this.onSubmitting = true;
    console.log(this.Entity);
    if (!this.isSubmit) alert('Dữ liệu nhập chưa hợp lệ'); return false;
  }
}

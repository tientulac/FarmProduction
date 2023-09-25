import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
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
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
    );
    this.Entity = new BrandEntity();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'brand';
    this.title.setTitle('Thương hiệu');
    this.field_Validation = {
      Code: false,
      Name: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;

    this.listName = [
      { id: 1, name: 'TienNN' },
      { id: 2, name: 'TienNN2' },
    ];
    // this.getList();
  }

  onSubmit() {
    this.onSubmitting = true;
    if (!this.isSubmit) alert('Dữ liệu nhập chưa hợp lệ'); return false;
  }

  openModal(type: any, data: any) {
    this.isInsert = true;
    data = new BrandEntity();
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }
}

import { Component } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent<T> {

  Entity!: T;
  Entities!: T[];
  URL: string = '';
  field_Validation: any = {};
  isSubmit: boolean = false;
  onSubmitting: boolean = false;
  isDisplayDelete: boolean = false;

  constructor(
    public baseService: BaseService<T>,
    public modal: NzModalService
  ) {
  }

  getList() {
    this.baseService.getAll(this.URL).subscribe(
      (res) => {
        this.Entities = res.Data;
      }
    );
  }

  save() {
    this.baseService.save(this.URL).subscribe(
      (res) => {
        this.Entity = res.Data;
      }
    );
  }

  getById() {
    this.baseService.getById(this.URL).subscribe(
      (res) => {
        this.Entity = res.Data;
      }
    );
  }

  validateCustom() {
    let values = Object.keys(this.field_Validation).map(key => this.field_Validation[key]);
    this.isSubmit = values.every(x => x == true);
  }

  handleCancel() {
    this.modal.closeAll();
    this.isDisplayDelete = false;
  }
}

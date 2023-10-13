import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent<T> {

  Entity!: T | null;
  Entities!: T[];
  URL: string = '';
  field_Validation: any = {};
  isSubmit: boolean = false;
  onSubmitting: boolean = false;
  isDisplayDelete: boolean = false;
  isFilter: boolean = false;
  isInsert: boolean = false;
  uploadFileName: any = '';

  GROUP_BUTTON = {
    EXCEL: false,
    FILTER: false,
    RELOAD: false,
    SEARCH: false,
    ADD: false
  }

  constructor(
    public baseService: BaseService<T>,
    public modal: NzModalService,
    public title: Title,
    public uploadImageService: UploadImageService
  ) {
  }


  filesUpload: NzUploadFile[] = [];

  listFileUpload = [...this.filesUpload];

  search() {
    this.baseService.search(this.URL).subscribe(
      (res) => {
        this.Entity = res.data;
      }
    );
  }

  getList() {
    this.baseService.getAll(this.URL).subscribe(
      (res) => {
        this.Entities = res.data;
      }
    );
  }

  save() {
    this.baseService.save(this.URL).subscribe(
      (res) => {
        this.Entity = res.data;
      }
    );
  }

  getById() {
    this.baseService.getById(this.URL).subscribe(
      (res) => {
        this.Entity = res.data;
      }
    );
  }

  validateCustom() {
    let values = Object.keys(this.field_Validation).map(key => this.field_Validation[key]);
    this.isSubmit = values.every(x => x == true);
  }

  handleCancel(): void {
    this.isDisplayDelete = false;
    this.isInsert = false;
    this.getList();
  }
}

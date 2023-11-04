import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})

export class BaseComponent<T> {

  Entity!: T | null | any;
  EntitySearch!: T;
  Entities!: T[];
  URL: string = '';
  field_Validation: any = {};
  isSubmit: boolean = false;
  onSubmitting: boolean = false;
  isDisplayDelete: boolean = false;
  isFilter: boolean = false;
  isInsert: boolean = false;
  uploadFileName: any = '';
  URL_Upload: any = '';
  id_record: any = null;

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
    public uploadImageService: UploadImageService,
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
    this.baseService.getAll(this.URL, this.EntitySearch).subscribe(
      (res) => {
        this.Entities = res.data;
      }
    );
  }

  save() {
    this.baseService.save(this.URL, this.Entity).subscribe(
      (res) => {
        if (res.code == "200") {
          this.Entity = res.data;
          alert("Thành công !");
          this.handleCancel();
        }
        else {
          alert(res.messageEX);
        }
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

  getImage(fileName: string) {
    this.uploadImageService.getImgUpload(fileName).subscribe(
      (res) => {
        console.log(res);
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

  handleUpload = (item: any) => {
    const formData = new FormData();
    formData.append(item.name, item.file as any, this.uploadFileName);
    this.uploadImageService.upload(formData).subscribe(
      (res: ReponseAPI<File>) => {
        item.onSuccess(item.file);
        if (res.code == "200") {
          this.Entity.image = res.message;
        }
        else {
          alert(res.message);
        }
      }
    );
  };

  beforeUpload = (file: NzUploadFile): boolean => {
    this.uploadFileName = `${this.URL}_${this.Entity?.code}.jpg`;
    return true;
  };
}

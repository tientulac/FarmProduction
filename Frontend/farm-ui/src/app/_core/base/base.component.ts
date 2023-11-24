import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { ToastrService } from 'ngx-toastr';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import * as _ from 'lodash';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})

export class BaseComponent<T> {

  Entity!: T | null | any;
  EntitySearch!: T | {};
  Entities!: T[];
  URL: string = '';
  field_Validation: any = {};
  isSubmit: boolean = false;
  onSubmitting: boolean = false;

  isDisplayDelete: boolean = false;
  isDisplayDetail: boolean = false;
  isDisplayAttribute: boolean = false;
  isShownInfo = false;
  isChangePass = false;
  isFilter: boolean = false;
  isInsert: boolean = false;

  uploadFileName: any = '';
  URL_Upload: any = '';
  id_record: any = null;
  isInsertDetail: boolean = false;
  id_edit: any = null;
  isEdit: boolean = false;
  dateRange: any;
  listStatus: any = [];

  cities: any;
  districts: any;
  wards: any;
  feeShip: any = 0;
  id_city: any = null;
  id_district: any = null;
  id_ward: any = null;

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
    public toastr: ToastrService
  ) {
  }


  filesUpload: NzUploadFile[] = [];

  listFileUpload = [...this.filesUpload];

  search() {
    // this.baseService.search(this.URL, this.EntitySearch).subscribe(
    //   (res) => {
    //     this.Entity = res.data;
    //   }
    // );
    this.getList();
  }

  getList() {
    this.baseService.getByRequest(this.URL, this.EntitySearch).subscribe(
      (res) => {
        this.Entities = res.data;
      }
    );
  }

  export() {
    this.baseService.export(this.URL).subscribe(
      (res) => {
        const linkSource = 'data:application/octet-stream;base64,' + res.data;
        const downloadLink = document.createElement('a');
        const fileName = `${this.URL}.xlsx`;
        downloadLink.href = linkSource;
        downloadLink.download = fileName;
        downloadLink.click();
      }
    );
  }

  getAll() {
    this.baseService.getAll(this.URL).subscribe(
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
          this.toastr.success("Thành công !");
          this.handleCancel();
        }
        else {
          this.toastr.warning(res.messageEX?.toString());
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
    this.isDisplayDetail = false;
    this.isDisplayAttribute = false;
    this.isShownInfo = false;
    this.isChangePass = false;
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
          console.log(this.listFileUpload);
        }
        else {
          this.toastr.warning(res.message?.toString());
        }
      }
    );
  };

  beforeUpload = (file: NzUploadFile): boolean => {
    this.uploadFileName = `${this.URL}_${this.Entity?.code}.jpg`;
    return true;
  };

  beforeUploadAttribute = (file: NzUploadFile): boolean => {
    this.uploadFileName = `image_${new Date().getTime()}.jpg`;
    return true;
  };

  displayFilter() {
    this.isFilter = !this.isFilter;
    this.EntitySearch = {};
    this.getList();
  }

  renderStatus(status: any) {
    return this.listStatus.filter((x: any) => x.id == status)[0].name ?? '';
  }

  getListCity() {
    this.baseService.getListCity().subscribe(
      (res) => {
        this.cities = res.data;
      }
    );;
  }

  getListDistrict() {
    this.baseService.getListDistrict({ province_id: parseInt(this.id_city) }).subscribe(
      (res) => {
        this.districts = _.sortBy(res.data, 'DistrictName');
      }
    );
  }

  getListWard() {
    this.baseService.getListWard({ district_id: parseInt(this.id_district) }).subscribe(
      (res) => {
        this.wards = res.data;
      }
    );
  }

  getPaymentShipper() {
    this.baseService.getShipPayment({
      "service_id": 100039,
      // "insurance_value": this.totalPrice,
      "coupon": null,
      "from_district_id": 1542,
      "to_district_id": parseInt(this.id_district),
      "to_ward_code": this.id_ward,
      "height": 5,
      "length": 5,
      "weight": 100,
      "width": 5
    }).subscribe(
      (res: any) => {
        this.feeShip = res.data.total;
      }
    );
  }
}

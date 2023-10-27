import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { BrandEntity } from 'src/app/entities/Brand.Entity';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';

interface ColumnItem {
  name: string;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn<any> | null;
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn<any> | null;
  filterMultiple: boolean;
  sortDirections: NzTableSortOrder[];
}

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
      AppInjector.get(UploadImageService),
    );
    this.Entity = new BrandEntity();
    this.Entities = new Array<BrandEntity>();;
    this.URL = 'brand';
    this.title.setTitle('Thương hiệu');
    this.field_Validation = {
      name: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;

    this.listName = [
      { id: 1, name: 'TienNN' },
      { id: 2, name: 'TienNN2' },
    ];
    this.getList();
  }

  async onSubmit(): Promise<boolean> {
    this.onSubmitting = true;
    if (!this.isSubmit) {
      alert('Dữ liệu nhập chưa hợp lệ');
      return false;
    }
    console.log(this.Entity);
    return true;
  }

  openModal(type: any, data: BrandEntity | null) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.Entity = new BrandEntity();
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }

  handleUpload = (item: any) => {
    const formData = new FormData();
    formData.append(item.name, item.file as any, this.uploadFileName);
    this.uploadImageService.upload(formData).subscribe(
      (res: ReponseAPI<File>) => {
        item.onSuccess(item.file);
        if (res.code == "200") {
          this.Entity!.image = res.message;
        }
        else {
          alert(res.message);
        }
      }
    );
  };

  beforeUpload = (file: NzUploadFile): boolean => {
    this.uploadFileName = `brand_${this.Entity?.code}.jpg`;
    return true;
  };

  listOfColumns: ColumnItem[] = [
    {
      name: 'Mã',
      sortOrder: null,
      sortFn: (a: BrandEntity, b: BrandEntity) => a.code.localeCompare(b.code),
      sortDirections: ['ascend', 'descend', null],
      filterMultiple: true,
      listOfFilter: [
        { text: 'Joe', value: 'Joe' },
        { text: 'Jim', value: 'Jim' }
      ],
      filterFn: (list: string[], item: BrandEntity) => list.some(x => item.code.indexOf(x) !== -1)
    },
    {
      name: 'Tên',
      sortOrder: null,
      sortFn: (a: BrandEntity, b: BrandEntity) => (a.name ?? '').localeCompare(b.name ?? ''),
      sortDirections: ['ascend', 'descend', null],
      filterMultiple: true,
      listOfFilter: [
        { text: 'Joe', value: 'Joe' },
        { text: 'Jim', value: 'Jim' }
      ],
      filterFn: (list: string[], item: BrandEntity) => list.some(x => (item.name ?? '').indexOf(x) !== -1)
    },
  ];
}

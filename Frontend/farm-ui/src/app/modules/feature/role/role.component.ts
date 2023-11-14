import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { RoleEntity, RoleEntitySearch } from 'src/app/entities/Role.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent extends BaseComponent<RoleEntity> {

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
  ) {
    super(
      AppInjector.get(BaseService<RoleEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new RoleEntity();
    this.EntitySearch = new RoleEntitySearch();
    this.Entities = new Array<RoleEntity>();;
    this.URL = 'role';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Quyền');
    this.field_Validation = {
      code: false,
      name: false
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.RELOAD = true;

    this.getList();
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

  openModal(type: any, data: RoleEntity) {
    this.isInsert = true;
    this.Entity = new RoleEntity();
    if (type === 'EDIT') {
      this.Entity = data;
    }
  }
}

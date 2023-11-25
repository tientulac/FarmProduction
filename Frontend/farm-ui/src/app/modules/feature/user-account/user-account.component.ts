import { Component, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { ReponseAPI } from 'src/app/entities/ResponseAPI';
import { RoleEntity } from 'src/app/entities/Role.Entity';
import { UserAccountEntity, UserAccountEntitySearch } from 'src/app/entities/UserAccount.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';
import { AppConfig, AppConfiguration } from 'src/configuration';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.scss']
})
export class UserAccountComponent extends BaseComponent<UserAccountEntity>{

  roles!: RoleEntity[];

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
    public roleService: BaseService<RoleEntity>,
  ) {
    super(
      AppInjector.get(BaseService<UserAccountEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.Entity = new UserAccountEntity();
    this.EntitySearch = new UserAccountEntitySearch();
    this.Entities = new Array<UserAccountEntity>();;
    this.URL = 'UserAccount';
    this.URL_Upload = appConfig.URL_UPLOAD;
    this.title.setTitle('Người dùng');
    this.field_Validation = {
      userName: false,
      hashPassword: false,
    };

    this.GROUP_BUTTON.ADD = true;
    this.GROUP_BUTTON.FILTER = true;
    this.GROUP_BUTTON.RELOAD = true;
    this.GROUP_BUTTON.SEARCH = true;
    this.GROUP_BUTTON.EXCEL = true;

    this.getList();
    this.getListRole();
    this.listStatus = [
      { name: 'Đang hoạt động', id: 1 },
      { name: 'Ẩn', id: 2 },
      { name: 'Vô hiệu hóa', id: 3 },
      { name: 'Đã duyệt', id: 4 },
      { name: 'Chờ duyệt', id: 5 },
    ];
  }

  getListRole(): void {
    this.roleService.getByRequest('role', {}).subscribe(
      (res: ReponseAPI<any>) => {
        this.roles = res.data;
      }
    );
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

  openModal(type: any, data: UserAccountEntity) {
    this.listFileUpload = [];
    this.isInsert = true;
    this.isEdit = false;
    this.Entity = new UserAccountEntity();
    if (type === 'EDIT') {
      this.isEdit = true;
      this.Entity = data;
      this.field_Validation.hashPassword = true;
    }
  }
}

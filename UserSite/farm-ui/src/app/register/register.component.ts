import { Component, Inject } from '@angular/core';
import { BaseComponent } from '../_core/base/base.component';
import { UserAccountEntity, UserAccountEntitySearch } from '../entities/UserAccount.Entity';
import { BaseService } from '../services/base.service';
import { RoleEntity } from '../entities/Role.Entity';
import { AppConfig, AppConfiguration } from 'src/configuration';
import { AppInjector } from '../app.module';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Title } from '@angular/platform-browser';
import { UploadImageService } from '../services/upload-image.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent<UserAccountEntity>{

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
    public roleService: BaseService<RoleEntity>,
    public router: Router
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
    this.title.setTitle('Đăng ký');
    this.listStatus = [
      { name: 'Đang hoạt động', id: 1 },
      { name: 'Ẩn', id: 2 },
      { name: 'Vô hiệu hóa', id: 3 },
      { name: 'Đã duyệt', id: 4 },
      { name: 'Chờ duyệt', id: 5 },
    ];
  }

  register() {
    this.save();
    this.router.navigateByUrl('/login');
  }
}

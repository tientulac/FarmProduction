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
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent<UserAccountEntity>{

  _userName: any = '';
  _password: any = '';

  constructor(
    @Inject(AppConfig) private readonly appConfig: AppConfiguration,
    public roleService: BaseService<RoleEntity>,
    public loginService: LoginService,
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


  login() {
    let req = {
      UserName: this._userName ?? '',
      Hashpassword: this._password ?? ''
    }
    if (this._userName == '' || this._password == '') {
      this.toastr.warning('Thông tin không được để trống');
    }
    else {
      this.loginService.login(req).subscribe(
        (res) => {
          if (res.code == "200") {
            if (res.data.status != 1) {
              this.toastr.warning('Tài khoản chưa được kích hoạt');
              return;
            }
            this.toastr.success("Đăng nhập thành công");
            localStorage.setItem('TOKEN', res.data.token?.toString() ?? '');
            localStorage.setItem('UserInfo', JSON.stringify(res.data) ?? {});
            window.location.href = '/';
          }
          else {
            this.toastr.warning('Tên đăng nhập hoặc mật khẩu không chính xác');
          }
        }
      );
    }
  }
}

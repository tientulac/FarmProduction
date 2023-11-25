import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  _userName: any = '';
  _password: any = '';

  constructor(
    public loginService: LoginService,
    public toastr: ToastrService,
    public router: Router
  ) {
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
            if (!res.data.role.code?.toLowerCase().includes('admin')) {
              this.toastr.warning('Tài khoản không có quyền truy cập quản trị');
              return;
            }
            this.toastr.success("Đăng nhập thành công");
            localStorage.setItem('TOKEN', res.data.token?.toString() ?? '');
            localStorage.setItem('UserInfo', JSON.stringify(res.data) ?? {});
            this.router.navigateByUrl('/main/feature/dashboard');
          }
          else {
            this.toastr.warning('Tên đăng nhập hoặc mật khẩu không chính xác');
          }
        }
      );
    }
  }
}

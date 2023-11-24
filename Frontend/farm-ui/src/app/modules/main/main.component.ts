import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/_core/base/base.component';
import { AppInjector } from 'src/app/app.module';
import { UserAccountEntity } from 'src/app/entities/UserAccount.Entity';
import { BaseService } from 'src/app/services/base.service';
import { UploadImageService } from 'src/app/services/upload-image.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends BaseComponent<UserAccountEntity>{

  isCollapsed = false;
  userInfo = new UserAccountEntity();

  constructor(
    public router: Router
  ) {
    super(
      AppInjector.get(BaseService<UserAccountEntity>),
      AppInjector.get(NzModalService),
      AppInjector.get(Title),
      AppInjector.get(UploadImageService),
      AppInjector.get(ToastrService),
    );
    this.userInfo = JSON.parse(JSON.parse(JSON.stringify(localStorage.getItem('UserInfo')))) ?? {};
    this.Entity = this.userInfo;
  }

  logout() {
    localStorage.clear();
    window.location.href = '/login';
  }

  openModal(type: any, data: UserAccountEntity) {
    this.Entity = data;
    if (type === 'INFO') {
      this.isShownInfo = true;
    }
    if (type === 'CHANGEPASS') {
      this.isChangePass = true;
    }
  }

  async onSubmit(): Promise<boolean> {
    this.onSubmitting = true;
    if (this.isShownInfo) {
      this.baseService.save('userAccount/update-info', this.Entity).subscribe(
        (res) => {
          if (res.code == '200') {
            this.toastr.success('Thành công');
            let data = JSON.parse(JSON.parse(JSON.stringify(localStorage.getItem('UserInfo'))));
            data.email = this.Entity.email;
            data.phone = this.Entity.phone;
            data.fullName = this.Entity.fullName;
            data.address = this.Entity.address;
            localStorage.setItem('UserInfo', JSON.stringify(data));
          }
          else {
            this.toastr.warning(res.message ?? '');
          }
        }
      );
    }
    if (this.isChangePass) {
      if (!this.Entity?.oldHashPassword || !this.Entity?.newHashPassword) {
        this.toastr.warning('Mật khẩu không được bỏ trống');
        return false;
      }
      if (this.Entity?.oldHashPassword != this.Entity?.newHashPassword) {
        this.toastr.warning('Xác nhận mật khẩu chưa đúng');
        return false;
      }
      this.baseService.save('userAccount/change-pass', this.Entity).subscribe(
        (res) => {
          if (res.code == '200') {
            this.toastr.success('Thành công. Hệ thống sẽ yêu cầu đăng nhập lại sau 3s');
            setTimeout(this.logout, 3000);
          }
          else {
            this.toastr.warning(res.message ?? '');
          }
        }
      );
    }
    return true;
  }

  override handleCancel(): void {
    this.isShownInfo = false;
    this.isChangePass = false;
    this.userInfo = JSON.parse(JSON.parse(JSON.stringify(localStorage.getItem('UserInfo')))) ?? {};
  }
}


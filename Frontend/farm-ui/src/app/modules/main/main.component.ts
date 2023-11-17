import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserAccountEntity } from 'src/app/entities/UserAccount.Entity';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  isCollapsed = false;
  isShownInfo = false;
  isChangePass = false;
  userInfo = new UserAccountEntity();

  constructor(
    public toastr: ToastrService,
    public router: Router
  ) {
    this.userInfo = JSON.parse(JSON.parse(JSON.stringify(localStorage.getItem('UserInfo'))));
  }


  logout() {
    localStorage.clear();
    this.toastr.success("Đăng xuất thành công");
    window.location.href = '/login';
  }
}

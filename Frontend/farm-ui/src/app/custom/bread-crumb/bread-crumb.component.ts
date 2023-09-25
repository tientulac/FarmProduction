import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bread-crumb',
  templateUrl: './bread-crumb.component.html',
  styleUrls: ['./bread-crumb.component.scss']
})
export class BreadCrumbComponent {

  textTitle = '';
  breadCrumbs: any = [];

  constructor(
    private title: Title,
    private route: ActivatedRoute
  ) {
    this.textTitle = this.title.getTitle();
    this.breadCrumbs = [
      {
        name: 'Trang chủ /',
        routerLink: '/',
        active: false,
        icon: 'fas fa-home',
      },
      {
        name: this.textTitle,
        routerLink: this.route.snapshot.routeConfig?.path,
        active: true,
        icon: ''
      },
    ];
  }
}

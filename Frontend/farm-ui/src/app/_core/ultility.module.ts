import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { CommonNgZorroAntdModule } from "../modules/ng-zorro-antd.module";
import { BaseComponent } from './base/base.component';
import { DeleteModalComponent } from "../custom/modal-custom/delete-modal/delete-modal.component";
import { GroupButtonComponent } from "../custom/group-button/group-button.component";
import { BreadCrumbComponent } from "../custom/bread-crumb/bread-crumb.component";

@NgModule({
  exports: [
    HttpClientModule,
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    CommonNgZorroAntdModule,
    DeleteModalComponent,
    GroupButtonComponent,
    BreadCrumbComponent,
  ],
  declarations: [
    BaseComponent,
    DeleteModalComponent,
    GroupButtonComponent,
    BreadCrumbComponent,
  ]
})
export class UtilityModule { }

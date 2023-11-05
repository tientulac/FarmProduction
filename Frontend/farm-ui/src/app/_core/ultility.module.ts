import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { CommonNgZorroAntdModule } from "../modules/ng-zorro-antd.module";
import { CustomModule } from "./custom.module";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    CommonNgZorroAntdModule,
    CustomModule
  ],
  exports: [
    FormsModule,
    CommonModule,
    CommonNgZorroAntdModule,
    CustomModule
  ],
  declarations: [
  ]
})
export class UtilityModule { }

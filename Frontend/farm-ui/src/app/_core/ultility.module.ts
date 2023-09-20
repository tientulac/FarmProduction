import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from "@angular/platform-browser";
import { CommonNgZorroAntdModule } from "../modules/ng-zorro-antd.module";
import { BaseComponent } from './base/base.component';

@NgModule({
    exports: [
        HttpClientModule,
        BrowserModule,
        ReactiveFormsModule,
        FormsModule,
        CommonModule,
        CommonNgZorroAntdModule
    ],
    declarations: [
    
    BaseComponent
  ],
})
export class UtilityModule { }

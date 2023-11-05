import { NgModule } from '@angular/core';
import { BaseComponent } from './base/base.component';
import { DeleteModalComponent } from "../custom/modal-custom/delete-modal/delete-modal.component";
import { GroupButtonComponent } from "../custom/group-button/group-button.component";
import { BreadCrumbComponent } from "../custom/bread-crumb/bread-crumb.component";
import { CommonNgZorroAntdModule } from '../modules/ng-zorro-antd.module';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputCustomComponent } from '../custom/input-custom/input-custom.component';
import { SelectCustomComponent } from '../custom/select-custom/select-custom.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from '../app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        CommonNgZorroAntdModule,
        HttpClientModule,
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
    ],
    declarations: [
        BaseComponent,
        DeleteModalComponent,
        GroupButtonComponent,
        BreadCrumbComponent,
        InputCustomComponent,
        SelectCustomComponent,
    ],
    exports: [
        BaseComponent,
        DeleteModalComponent,
        GroupButtonComponent,
        BreadCrumbComponent,
        InputCustomComponent,
        SelectCustomComponent,
    ],
})
export class CustomModule { }

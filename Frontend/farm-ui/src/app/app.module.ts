import { Injector, NgModule } from "@angular/core";
import { UtilityModule } from "./_core/ultility.module";
import { AppRoutingModule } from "./app.routing.module";
import { NZ_I18N, en_US, vi_VN } from "ng-zorro-antd/i18n";
import { AppComponent } from "./app.component";
import { registerLocaleData } from '@angular/common';
import vi from '@angular/common/locales/vi';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainModule } from "./modules/main/main.module";
import { BaseService } from "./services/base.service";
import { BrowserModule } from "@angular/platform-browser";
import { ToastrModule } from 'ngx-toastr';
import { SetHeaderInterceptor } from "./interceptor/set-header.interceptor";
import { LoginComponent } from "./modules/login/login.component";
import { DashboardComponent } from "./modules/dashboard/dashboard.component";
import { RouterModule } from "@angular/router";

registerLocaleData(vi);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    AppRoutingModule,
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    MainModule,
    UtilityModule,
    RouterModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),],
  providers: [
    { provide: NZ_I18N, useValue: vi_VN },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SetHeaderInterceptor,
      multi: true
    },
    BaseService
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(private injector: Injector) {
    AppInjector = this.injector;
  }
}

export let AppInjector: Injector;

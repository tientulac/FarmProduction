import { Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { vi_VN } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import vi from '@angular/common/locales/vi';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonNgZorroAntdModule } from './ng-zorro-antd.module';
import { BaseComponent } from './_core/base/base.component';
import { HomePageComponent } from './home-page/home-page.component';
import { HeaderComponent } from './home-page/header/header.component';
import { FooterComponent } from './home-page/footer/footer.component';
import { MainComponent } from './home-page/main/main.component';
import { BaseService } from './services/base.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';

registerLocaleData(vi);

@NgModule({
  declarations: [
    AppComponent,
    BaseComponent,
    HomePageComponent,
    HeaderComponent,
    FooterComponent,
    MainComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CommonNgZorroAntdModule,
    ToastrModule.forRoot(), // ToastrModule added
  ],
  providers: [
    BaseService,
    { provide: NZ_I18N, useValue: vi_VN },
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(private injector: Injector) {
    AppInjector = this.injector;
  }
}

export let AppInjector: Injector;

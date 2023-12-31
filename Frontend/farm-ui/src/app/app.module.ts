import { Injector, NgModule } from "@angular/core";
import { UtilityModule } from "./_core/ultility.module";
import { AppRoutingModule } from "./app.routing.module";
import { NZ_I18N, en_US, vi_VN } from "ng-zorro-antd/i18n";
import { AppComponent } from "./app.component";
import { registerLocaleData } from '@angular/common';
import vi from '@angular/common/locales/vi';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainModule } from "./modules/main/main.module";
import { BaseService } from "./services/base.service";

registerLocaleData(vi);

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    UtilityModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MainModule
  ],
  providers: [
    { provide: NZ_I18N, useValue: vi_VN },
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

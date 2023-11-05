import { NgModule } from "@angular/core";
import { MainRoutingModule } from "./main.routing.module";
import { HeaderComponent } from "src/app/layouts/header/header.component";
import { FooterComponent } from "src/app/layouts/footer/footer.component";
import { SidebarComponent } from "src/app/layouts/sidebar/sidebar.component";
import { MainComponent } from "./main.component";
import { CommonNgZorroAntdModule } from "../ng-zorro-antd.module";

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        SidebarComponent,
        MainComponent,
    ],
    imports: [
        MainRoutingModule,
        CommonNgZorroAntdModule,
    ],
    providers: [
    ],
})

export class MainModule { }

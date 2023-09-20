import { NgModule } from "@angular/core";
import { CommonNgZorroAntdModule } from "../ng-zorro-antd.module";
import { BrandComponent } from "./brand/brand.component";
import { CategoryRoutingModule } from "./category.routing.module";
import { BaseService } from "src/app/services/base.service";
import { InputCustomComponent } from "src/app/custom/input-custom/input-custom.component";
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [
        BrandComponent,
        InputCustomComponent
    ],
    imports: [
        CategoryRoutingModule,
        CommonNgZorroAntdModule,
        FormsModule,
    ],
    providers: [
        BaseService
    ],
})

export class CategoryModule { }

import { NgModule } from "@angular/core";
import { CommonNgZorroAntdModule } from "../ng-zorro-antd.module";
import { BrandComponent } from "./brand/brand.component";
import { CategoryRoutingModule } from "./category.routing.module";
import { BaseService } from "src/app/services/base.service";
import { InputCustomComponent } from "src/app/custom/input-custom/input-custom.component";
import { FormsModule } from "@angular/forms";
import { UtilityModule } from "src/app/_core/ultility.module";

@NgModule({
    declarations: [
        BrandComponent,
        InputCustomComponent
    ],
    imports: [
        CategoryRoutingModule,
        UtilityModule
    ],
    providers: [
        BaseService
    ],
})

export class CategoryModule { }

import { NgModule } from "@angular/core";
import { BaseService } from "src/app/services/base.service";
import { UtilityModule } from "src/app/_core/ultility.module";
import { ProductRoutingModule } from "./product.routing.module";
import { ProductComponent } from "./product/product.component";

@NgModule({
    declarations: [
        ProductComponent,
    ],
    imports: [
        ProductRoutingModule,
        UtilityModule
    ],
    providers: [
        BaseService
    ],
})

export class ProductModule { }

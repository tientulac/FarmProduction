import { NgModule } from "@angular/core";
import { BaseService } from "src/app/services/base.service";
import { UtilityModule } from "src/app/_core/ultility.module";
import { ProductRoutingModule } from "./product.routing.module";
import { ProductComponent } from "./product/product.component";
import { StatisticComponent } from "./statistic/statistic.component";
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
    declarations: [
        ProductComponent,
        StatisticComponent
    ],
    imports: [
        ProductRoutingModule,
        CKEditorModule,
        UtilityModule
    ],
    providers: [
        BaseService
    ],
})

export class ProductModule { }

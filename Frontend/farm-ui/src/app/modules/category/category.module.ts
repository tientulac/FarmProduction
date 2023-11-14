import { NgModule } from "@angular/core";
import { BrandComponent } from "./brand/brand.component";
import { CategoryRoutingModule } from "./category.routing.module";
import { BaseService } from "src/app/services/base.service";
import { UtilityModule } from "src/app/_core/ultility.module";
import { CatalogComponent } from "./catalog/catalog.component";
import { ProducerComponent } from "./producer/producer.component";

@NgModule({
    declarations: [
        BrandComponent,
        CatalogComponent,
        ProducerComponent
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

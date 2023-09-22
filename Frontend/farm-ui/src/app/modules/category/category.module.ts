import { NgModule } from "@angular/core";
import { BrandComponent } from "./brand/brand.component";
import { CategoryRoutingModule } from "./category.routing.module";
import { BaseService } from "src/app/services/base.service";
import { InputCustomComponent } from "src/app/custom/input-custom/input-custom.component";
import { UtilityModule } from "src/app/_core/ultility.module";
import { SelectCustomComponent } from "src/app/custom/select-custom/select-custom.component";
import { DeleteModalComponent } from "src/app/custom/modal-custom/delete-modal/delete-modal.component";

@NgModule({
    declarations: [
        BrandComponent,
        InputCustomComponent,
        SelectCustomComponent,
        DeleteModalComponent
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

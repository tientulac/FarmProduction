import { NgModule } from "@angular/core";
import { BaseService } from "src/app/services/base.service";
import { UtilityModule } from "src/app/_core/ultility.module";
import { RoleComponent } from "./role/role.component";
import { UserAccountComponent } from "./user-account/user-account.component";
import { OrderComponent } from "./order/order.component";
import { FeatureRoutingModule } from "./feature.routing.module";
import { ShoppingComponent } from "./shopping/shopping.component";

@NgModule({
    declarations: [
        RoleComponent,
        UserAccountComponent,
        OrderComponent,
        ShoppingComponent
    ],
    imports: [
        FeatureRoutingModule,
        UtilityModule
    ],
    providers: [
        BaseService
    ],
})

export class FeatureModule { }

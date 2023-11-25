import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleComponent } from './role/role.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { OrderComponent } from './order/order.component';
import { ShoppingComponent } from './shopping/shopping.component';
import { DashboardComponent } from '../dashboard/dashboard.component';

const routes: Routes = [
    { path: 'role', component: RoleComponent },
    { path: 'user-account', component: UserAccountComponent },
    { path: 'order', component: OrderComponent },
    { path: 'shopping', component: ShoppingComponent },
    { path: 'dashboard', component: DashboardComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FeatureRoutingModule { }

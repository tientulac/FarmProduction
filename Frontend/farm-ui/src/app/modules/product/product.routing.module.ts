import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './product/product.component';
import { StatisticComponent } from './statistic/statistic.component';

const routes: Routes = [
    { path: '', component: ProductComponent },
    { path: 'statistic', component: StatisticComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductRoutingModule { }

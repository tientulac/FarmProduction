import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { LoginComponent } from '../login/login.component';

const routes: Routes = [
    {
        path: 'main',
        component: MainComponent,
        children: [
            {
                path: 'category',
                loadChildren: () => import('../category/category.module').then(m => m.CategoryModule)
            },
            {
                path: 'product',
                loadChildren: () => import('../product/product.module').then(m => m.ProductModule)
            },
            {
                path: 'feature',
                loadChildren: () => import('../feature/feature.module').then(m => m.FeatureModule)
            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class MainRoutingModule { }

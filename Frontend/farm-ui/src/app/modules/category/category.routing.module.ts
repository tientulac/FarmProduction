import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrandComponent } from './brand/brand.component';
import { CatalogComponent } from './catalog/catalog.component';
import { ProducerComponent } from './producer/producer.component';

const routes: Routes = [
    { path: 'brand', component: BrandComponent },
    { path: 'catalog', component: CatalogComponent },
    { path: 'producer', component: ProducerComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryRoutingModule { }

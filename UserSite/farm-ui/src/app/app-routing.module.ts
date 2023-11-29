import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { MainComponent } from './home-page/main/main.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactComponent } from './contact/contact.component';
import { CategoryComponent } from './category/category.component';

const routes: Routes = [
  {
    path: '', component: HomePageComponent,
    children: [
      { path: '', component: MainComponent },
      { path: 'about-us', component: AboutUsComponent },
      { path: 'contact', component: ContactComponent },
      { path: 'category/:id', component: CategoryComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

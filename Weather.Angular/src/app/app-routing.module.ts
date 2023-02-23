import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CitydetailsComponent } from './components/citydetails/citydetails.component';

const routes: Routes = [
  {
    path: '',
    component: CitydetailsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

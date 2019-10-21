// import { NgModule } from '@angular/core';
// import { RouterModule } from '@angular/router';
// import { HomeRoutes } from './home-routing.routing';
// import { HomeComponent } from './home.component';

// @NgModule({
//     imports: [
//         RouterModule.forChild(HomeRoutes),
//     ],
//     declarations: [HomeComponent]
// })
// export class HomeModule { }


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }

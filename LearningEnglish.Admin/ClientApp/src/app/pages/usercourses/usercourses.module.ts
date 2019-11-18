import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { UsercoursesComponent } from './usercourses.component';
import { UsercoursesRoutingModule } from './usercourses-routing.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      UsercoursesRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      UsercoursesComponent,
  ]
})
export class UsercoursesModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { AchievementsComponent } from './achievements.component';
import { AchievementsRoutingModule } from './achievements-routing.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      AchievementsRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      AchievementsComponent,
  ]
})
export class AchievementsModule { }

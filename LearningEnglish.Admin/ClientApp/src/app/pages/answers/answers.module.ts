import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnswersComponent } from './answers.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AnswersRoutingModule } from './answers-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddAnswerComponent } from './add-answer/add-answer.component';
import { EditAnswerComponent } from './edit-answer/edit-answer.component';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      AnswersRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      AnswersComponent,
      AddAnswerComponent,
      EditAnswerComponent
  ]
})
export class AnswersModule { }

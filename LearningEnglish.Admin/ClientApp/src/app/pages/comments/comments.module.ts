import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommentsComponent } from './comments.component';
import { CommentsRoutingModule } from './comments-routing.module';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      CommentsRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      CommentsComponent,
  ]
})
export class CommentsModule { }

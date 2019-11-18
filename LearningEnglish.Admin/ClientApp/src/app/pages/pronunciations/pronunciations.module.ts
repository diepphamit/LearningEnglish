import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PronunciationsComponent } from './pronunciations.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PronunciationsRoutingModule } from './pronunciations-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddPronunciationComponent } from './add-pronunciation/add-pronunciation.component';
import { EditPronunciationComponent } from './edit-pronunciation/edit-pronunciation.component';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      PronunciationsRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      PronunciationsComponent,
      AddPronunciationComponent,
      EditPronunciationComponent
  ]
})
export class PronunciationsModule { }

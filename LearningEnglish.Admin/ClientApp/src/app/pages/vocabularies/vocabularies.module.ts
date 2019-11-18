import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VocabulariesComponent } from './vocabularies.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VocabulariesRoutingModule } from './vocabularies-routing.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddVocabularyComponent } from './add-vocabulary/add-vocabulary.component';
import { EditVocabularyComponent } from './edit-vocabulary/edit-vocabulary.component';

@NgModule({
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      VocabulariesRoutingModule,
      NgxPaginationModule
  ],
  declarations: [
      VocabulariesComponent,
      AddVocabularyComponent,
      EditVocabularyComponent
  ]
})
export class VocabulariesModule { }

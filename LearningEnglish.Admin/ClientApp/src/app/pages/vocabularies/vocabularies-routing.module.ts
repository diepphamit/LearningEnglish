import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { VocabulariesComponent } from './vocabularies.component';
import { AddVocabularyComponent } from './add-vocabulary/add-vocabulary.component';
import { EditVocabularyComponent } from './edit-vocabulary/edit-vocabulary.component';


export const routes: Routes = [
    {
        path: '', component: VocabulariesComponent,
    },
    {
        path: 'add', component: AddVocabularyComponent
    },
    {
        path: 'edit/:id', component: EditVocabularyComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class VocabulariesRoutingModule { }

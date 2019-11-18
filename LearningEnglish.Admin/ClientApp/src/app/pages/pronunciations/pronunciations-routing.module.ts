import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { PronunciationsComponent } from './pronunciations.component';
import { AddPronunciationComponent } from './add-pronunciation/add-pronunciation.component';
import { EditPronunciationComponent } from './edit-pronunciation/edit-pronunciation.component';


export const routes: Routes = [
    {
        path: '', component: PronunciationsComponent,
    },
    {
        path: 'add', component: AddPronunciationComponent
    },
    {
        path: 'edit/:id', component: EditPronunciationComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class PronunciationsRoutingModule { }

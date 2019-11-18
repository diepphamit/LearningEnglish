import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AnswersComponent } from './answers.component';
import { AddAnswerComponent } from './add-answer/add-answer.component';
import { EditAnswerComponent } from './edit-answer/edit-answer.component';


export const routes: Routes = [
    {
        path: '', component: AnswersComponent,
    },
    {
        path: 'add', component: AddAnswerComponent
    },
    {
        path: 'edit/:id', component: EditAnswerComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AnswersRoutingModule { }

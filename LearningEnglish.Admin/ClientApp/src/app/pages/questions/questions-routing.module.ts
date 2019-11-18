import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { QuestionsComponent } from './questions.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { EditQuestionComponent } from './edit-question/edit-question.component';


export const routes: Routes = [
    {
        path: '', component: QuestionsComponent,
    },
    {
        path: 'add', component: AddQuestionComponent
    },
    {
        path: 'edit/:id', component: EditQuestionComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class QuestionsRoutingModule { }

import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { LessonsComponent } from './lessons.component';
import { AddLessonComponent } from './add-lesson/add-lesson.component';
import { EditLessonComponent } from './edit-lesson/edit-lesson.component';


export const routes: Routes = [
    {
        path: '', component: LessonsComponent,
    },
    {
        path: 'add', component: AddLessonComponent
    },
    {
        path: 'edit/:id', component: EditLessonComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class LessonsRoutingModule { }

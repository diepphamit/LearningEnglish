import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CoursesComponent } from './courses.component';
import { AddCourseComponent } from './add-course/add-course.component';
import { EditCourseComponent } from './edit-course/edit-course.component';

export const routes: Routes = [
    {
        path: '', component: CoursesComponent,
    },
    {
        path: 'add', component: AddCourseComponent
    },
    {
        path: 'edit/:id', component: EditCourseComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class CoursesRoutingModule { }

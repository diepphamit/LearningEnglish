import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { LessonsRoutingModule } from './lessons-routing.module';
import { LessonsComponent } from './lessons.component';
import { AddLessonComponent } from './add-lesson/add-lesson.component';
import { EditLessonComponent } from './edit-lesson/edit-lesson.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        LessonsRoutingModule,
        NgxPaginationModule
    ],
    declarations: [
        LessonsComponent,
        AddLessonComponent,
        EditLessonComponent
    ]
})
export class LessonsModule { }

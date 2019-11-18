import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { UsercoursesComponent } from './usercourses.component';




export const routes: Routes = [
    {
        path: '', component: UsercoursesComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class UsercoursesRoutingModule { }

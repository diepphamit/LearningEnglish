import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommentsComponent } from './comments.component';




export const routes: Routes = [
    {
        path: '', component: CommentsComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class CommentsRoutingModule { }

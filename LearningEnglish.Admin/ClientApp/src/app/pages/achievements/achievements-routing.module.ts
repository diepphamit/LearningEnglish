import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AchievementsComponent } from './achievements.component';




export const routes: Routes = [
    {
        path: '', component: AchievementsComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AchievementsRoutingModule { }

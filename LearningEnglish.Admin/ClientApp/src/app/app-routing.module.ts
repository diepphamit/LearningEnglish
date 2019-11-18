import { FullComponent } from './layouts/full/full.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlankComponent } from './layouts/blank/blank.component';
import { AuthGuard } from './services/auth-guard.service';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: '',
        component: BlankComponent,
        children: [
            {
                path: 'login',
                loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule)
            }
        ]
    },
    {
        path: '',
        component: FullComponent,
        canActivateChild: [AuthGuard],
        children: [
            {
                path: 'home',
                loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
            },
            {
                path: 'users',
                loadChildren: () => import('./pages/users-management/users-management.module').then(m => m.UsersManagementModule)
            },
            {
                path: 'courses',
                loadChildren: () => import('./pages/courses/courses.module').then(m => m.CoursesModule)
            },
            {
                path: 'lessons',
                loadChildren: () => import('./pages/lessons/lessons.module').then(m => m.LessonsModule)
            },
            {
                path: 'vocabularies',
                loadChildren: () => import('./pages/vocabularies/vocabularies.module').then(m => m.VocabulariesModule)
            },
            {
                path: 'pronunciations',
                loadChildren: () => import('./pages/pronunciations/pronunciations.module').then(m => m.PronunciationsModule)
            },
            {
                path: 'questions',
                loadChildren: () => import('./pages/questions/questions.module').then(m => m.QuestionsModule)
            },
            {
                path: 'answers',
                loadChildren: () => import('./pages/answers/answers.module').then(m => m.AnswersModule)
            },
            {
                path: 'comments',
                loadChildren: () => import('./pages/comments/comments.module').then(m => m.CommentsModule)
            },
            {
                path: 'achievements',
                loadChildren: () => import('./pages/achievements/achievements.module').then(m => m.AchievementsModule)
            },
            {
                path: 'userCourses',
                loadChildren: () => import('./pages/usercourses/usercourses.module').then(m => m.UsercoursesModule)
            }
        ]
    },
    {
        path: '**',
        redirectTo: 'login'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }

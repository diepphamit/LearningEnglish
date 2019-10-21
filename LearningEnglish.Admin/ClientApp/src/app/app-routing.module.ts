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
            }
        ]
    },
    {
        path: '',
        component: FullComponent,
        canActivateChild: [AuthGuard],
        children: [
            {
                path: 'users',
                loadChildren: () => import('./pages/users-management/users-management.module').then(m => m.UsersManagementModule)
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

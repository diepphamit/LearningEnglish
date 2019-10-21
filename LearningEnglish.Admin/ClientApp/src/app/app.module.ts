import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BlankComponent } from './layouts/blank/blank.component';
import { FullComponent } from './layouts/full/full.component';
import { CoreModule } from './core/core.module';
import { ACCESS_TOKEN } from './constants/db-keys';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

@NgModule({
    declarations: [
        AppComponent,
        BlankComponent,
        FullComponent
    ],
    imports: [
        CoreModule,
        AppRoutingModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
        }),
        JwtModule.forRoot({
            config: {
                tokenGetter: () => {
                    return localStorage.getItem(ACCESS_TOKEN);
                },
                whitelistedDomains: [
                    'localhost:5000',
                    'localhost:44327'
                ],
                blacklistedRoutes: [
                    'localhost:5000/api/auth/login',
                    'localhost:44327/api/auth/login'
                ]
            }
        }),
        TooltipModule.forRoot(),
        ModalModule.forRoot()
    ],
    providers: [
        AuthGuard,
        AuthService,
        UserService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersManagementComponent } from './users-management.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersManagementRoutingModule } from './users-management-routing.module';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { EditfullUserComponent } from './editfull-user/editfull-user.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        UsersManagementRoutingModule,
        NgxPaginationModule,
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        UsersManagementComponent,
        AddUserComponent,
        EditUserComponent,
        EditfullUserComponent
    ]
})
export class UsersManagementModule { }

import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user/user.model';
import { Utilities } from 'src/app/services/utilities';

@Component({
    selector: 'app-add-user',
    templateUrl: './add-user.component.html'
})
export class AddUserComponent implements OnInit {

    addUserForm: FormGroup;
    user: User;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private userService: UserService,
        private toastr: ToastrService
    ) {
        this.addUserForm = this.fb.group({
            userName: ['', Validators.required],
            fullName: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    ngOnInit() {
    }

    addUser() {
        this.user = Object.assign({}, this.addUserForm.value);
        this.user.roles = ['Admin'];

        this.userService.createUser(this.user).subscribe(
            () => {
                this.router.navigate(['/users']).then(() => {
                    this.toastr.success('Tạo tài khoản thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Tạo tài khoản không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.addUserForm.controls; }
}

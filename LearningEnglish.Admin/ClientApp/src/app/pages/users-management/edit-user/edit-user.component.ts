import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user/user.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
    selector: 'app-edit-user',
    templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {

    editUserForm: FormGroup;
    user: User;
    id: any;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private userService: UserService,
        private toastr: ToastrService
    ) {
        this.editUserForm = this.fb.group({
            userName: ['', Validators.required],
            fullName: ['', Validators.required],
            email: ['', Validators.required]
        });
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.userService.getUserById(this.id).subscribe(
                    result => {
                        this.user = result;
                        this.editUserForm.controls.userName.setValue(result.userName);
                        this.editUserForm.controls.fullName.setValue(result.fullName);
                        this.editUserForm.controls.email.setValue(result.email);
                    },
                    error => {
                        this.toastr.error(`Không tìm thấy tài khoản này`);
                    });
            }
        });
    }

    editUser() {
        this.user = Object.assign({}, this.editUserForm.value);
        this.user.roles = ['Admin'];

        this.userService.editUser(this.id, this.user).subscribe(
            () => {
                this.router.navigate(['/users']).then(() => {
                    this.toastr.success('Cập nhật tài khoản thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Cập nhật tài khoản không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.editUserForm.controls; }
}

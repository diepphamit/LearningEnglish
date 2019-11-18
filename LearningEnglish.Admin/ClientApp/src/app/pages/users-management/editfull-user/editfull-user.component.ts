import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user/user.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
    selector: 'app-editfull-user',
    templateUrl: './editfull-user.component.html'
})
export class EditfullUserComponent implements OnInit {

    editUserForm: FormGroup;
    user: User;
    id: any;
    gen: boolean;
    genders: any[] = [
        { key: false, value: ['Nữ'] },
        { key: true, value: ['Nam'] }
    ]

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private userService: UserService,
        private toastr: ToastrService
    ) {
        this.editUserForm = this.fb.group({
            fullName: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            address: ['', Validators.required],
            gender: ['', Validators.required],
            dateOfBirth: ['', Validators.required]

        });
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.userService.getUserFullById(this.id).subscribe(
                    result => {
                        this.user = result;
                        this.editUserForm.controls.fullName.setValue(result.fullName);
                        this.editUserForm.controls.phoneNumber.setValue(result.phoneNumber);
                        this.editUserForm.controls.address.setValue(result.address);
                        this.editUserForm.controls.gender.setValue(result.gender==="True");
                        this.editUserForm.controls.dateOfBirth.setValue(new Date(result.dateOfBirth));
                    },
                    error => {
                        this.toastr.error(`Không tìm thấy tài khoản này`);
                    });
            }
        });
    }

    editUser() {
        this.user = Object.assign({}, this.editUserForm.value);
        this.user.gender = this.gen;
        this.userService.editUserFull(this.id, this.user).subscribe(
            () => {
                this.router.navigate(['/home']).then(() => {
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

    genderFilter(value:boolean){
        this.gen = value;
    }
}

import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user/user.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Utilities } from 'src/app/services/utilities';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
    selector: 'app-users-management',
    templateUrl: './users-management.component.html'
})
export class UsersManagementComponent implements OnInit {

    modalRef: BsModalRef;
    user: User;
    keyword: string;
    page: number;
    pageSize: number;
    total: number;
    itemsAsync: Observable<any[]>;

    constructor(
        public userService: UserService,
        private router: Router,
        private modalService: BsModalService,
        private toastr: ToastrService
    ) { }

    ngOnInit() {
        this.keyword = '';
        this.page = 1;
        this.pageSize = 10;
        this.getAllUsers(this.page);
    }

    getAllUsers(page: number) {
        this.itemsAsync = this.userService.getAllUsers(this.keyword, page, this.pageSize)
            .pipe(
                tap(response => {
                    this.total = response.total;
                    this.page = page;
                }),
                map(response => response.items)
            );
    }

    add() {
        this.router.navigate(['/users/add']);
    }

    edit(id: any) {
        this.router.navigate(['/users/edit/' + id]);
    }

    deleteConfirm(template: TemplateRef<any>, data: any) {
        this.user = Object.assign({}, data);
        this.modalRef = this.modalService.show(template);
    }

    confirm(): void {
        if (this.user) {
            this.userService.deleteUser(this.user.id)
                .subscribe(
                    () => {
                        this.getAllUsers(this.page);
                        this.toastr.success(`Xóa tài khoản thành công`);
                    },
                    (error: HttpErrorResponse) => {
                        const errors = Utilities.getErrorResponses(error);

                        if (!errors.length) {
                            errors.push(`Xóa tài khoản không thành công!`);
                        }

                        this.toastr.error(errors.join(','));
                    }
                );
        }
        this.user = undefined;
        this.modalRef.hide();
    }

    close(): void {
        this.user = undefined;
        this.modalRef.hide();
    }

    search() {
        this.getAllUsers(this.page);
    }

    refresh() {
        this.keyword = '';
        this.getAllUsers(this.page);
    }
}

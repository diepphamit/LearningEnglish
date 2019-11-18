import { Component, OnInit, TemplateRef } from '@angular/core';
import { Course } from 'src/app/models/course/course.model';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/services/course.service';
import { tap, map, debounceTime } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-courses',
    templateUrl: './courses.component.html'
})
export class CoursesComponent implements OnInit {

    modalRef: BsModalRef;
    course: Course;
    keyword: string;
    page: number;
    pageSize: number;
    total: number;
    itemsAsync: Observable<any[]>;

    constructor(
        private courseService: CourseService,
        private router: Router,
        private modalService: BsModalService,
        private toastr: ToastrService
    ) { }

    ngOnInit() {
        this.keyword = '';
        this.page = 1;
        this.pageSize = 10;
        this.getAllCourses(this.page);
    }

    getAllCourses(page: number) {
        this.itemsAsync = this.courseService.getAllCourses(this.keyword, page, this.pageSize)
            .pipe(
                tap(response => {
                    this.total = response.total;
                    this.page = page;
                }),
                map(response => response.items)
            );
    }

    add() {
        this.router.navigate(['/courses/add']);
    }

    edit(id: any) {
        this.router.navigate(['/courses/edit/' + id]);
    }

    deleteConfirm(template: TemplateRef<any>, data: any) {
        this.course = Object.assign({}, data);
        this.modalRef = this.modalService.show(template);
    }

    confirm(): void {
        if (this.course) {
            this.courseService.deleteCourse(this.course.id)
                .subscribe(
                    () => {
                        this.getAllCourses(this.page);
                        this.toastr.success(`Xóa khóa học thành công`);
                    },
                    (error: HttpErrorResponse) => {
                        const errors = Utilities.getErrorResponses(error);

                        if (!errors.length) {
                            errors.push(`Xóa khóa học không thành công!`);
                        }

                        this.toastr.error(errors.join(','));
                    }
                );
        }
        this.course = undefined;
        this.modalRef.hide();
    }

    close(): void {
        this.course = undefined;
        this.modalRef.hide();
    }

    search() {
        this.getAllCourses(this.page);
    }

    searchCharacter() {
        this.itemsAsync = this.courseService.getAllCourses(this.keyword, this.page, this.pageSize)
            .pipe(
                tap(response => {
                    this.total = response.total;
                }),
                map(response => response.items),
                debounceTime(1000)    
            );
    }

    refresh() {
        this.keyword = '';
        this.getAllCourses(this.page);
    }

}

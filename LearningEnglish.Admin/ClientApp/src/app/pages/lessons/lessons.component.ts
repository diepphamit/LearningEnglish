import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/services/course.service';
import { tap, map, debounceTime } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Lesson } from 'src/app/models/lesson/lesson.model';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
    selector: 'app-lessons',
    templateUrl: './lessons.component.html'
})
export class LessonsComponent implements OnInit {

    modalRef: BsModalRef;
    lesson: Lesson;
    keyword: string;
    page: number;
    pageSize: number;
    courses: Observable<any[]>;
    total: number;
    itemsAsync: Observable<any[]>;

    constructor(
        private lessonService: LessonService,
        private courseService: CourseService,
        private router: Router,
        private modalService: BsModalService,
        private toastr: ToastrService
    ) { }

    ngOnInit() {
        this.keyword = '';
        this.page = 1;
        this.pageSize = 10;
        this.getAllLessons(this.page);
        this.courses = this.courseService.getAllCourseName();
    }

    getAllLessons(page: number) {
        this.itemsAsync = this.lessonService.getAllLessons(this.keyword, page, this.pageSize)
            .pipe(
                tap(response => {
                    this.total = response.total;
                    this.page = page;
                }),
                map(response => response.items)
            );
    }

    add() {
        this.router.navigate(['/lessons/add']);
    }

    edit(id: any) {
        this.router.navigate(['/lessons/edit/' + id]);
    }

    deleteConfirm(template: TemplateRef<any>, data: any) {
        this.lesson = Object.assign({}, data);
        this.modalRef = this.modalService.show(template);
    }

    confirm(): void {
        if (this.lesson) {
            this.lessonService.deleteLesson(this.lesson.id)
                .subscribe(
                    () => {
                        this.getAllLessons(this.page);
                        this.toastr.success(`Xóa bài học thành công`);
                    },
                    (error: HttpErrorResponse) => {
                        const errors = Utilities.getErrorResponses(error);

                        if (!errors.length) {
                            errors.push(`Xóa bài học không thành công!`);
                        }

                        this.toastr.error(errors.join(','));
                    }
                );
        }
        this.lesson = undefined;
        this.modalRef.hide();
    }

    close(): void {
        this.lesson = undefined;
        this.modalRef.hide();
    }

    search() {
        this.getAllLessons(this.page);
    }

    searchCharacter() {
        this.itemsAsync = this.lessonService.getAllLessons(this.keyword, this.page, this.pageSize)
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
        this.getAllLessons(this.page);
    }

    filterCourses(filterVal: any){
        if(filterVal !="0") this.keyword = filterVal;
        else this.keyword = '';
        this.getAllLessons(this.page);
        this.keyword = '';
        
    }

}

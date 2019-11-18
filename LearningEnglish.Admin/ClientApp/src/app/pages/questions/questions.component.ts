import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/services/course.service';
import { tap, map, debounceTime } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Question } from 'src/app/models/question/question.model';
import { QuestionService } from 'src/app/services/question.service';

@Component({
    selector: 'app-questions',
    templateUrl: './questions.component.html'
})
export class QuestionsComponent implements OnInit {

    modalRef: BsModalRef;
    question: Question;
    keyword: string;
    page: number;
    pageSize: number;
    courses: Observable<any[]>;
    total: number;
    itemsAsync: Observable<any[]>;

    constructor(
        private questionService: QuestionService,
        private courseService: CourseService,
        private router: Router,
        private modalService: BsModalService,
        private toastr: ToastrService
    ) { }

    ngOnInit() {
        this.keyword = '';
        this.page = 1;
        this.pageSize = 10;
        this.getAllQuestions(this.page);
        this.courses = this.courseService.getAllCourseName();
    }

    getAllQuestions(page: number) {
        this.itemsAsync = this.questionService.getAllQuestions(this.keyword, page, this.pageSize)
            .pipe(
                tap(response => {
                    this.total = response.total;
                    this.page = page;
                }),
                map(response => response.items)
            );
    }

    add() {
        this.router.navigate(['/questions/add']);
    }

    edit(id: any) {
        this.router.navigate(['/questions/edit/' + id]);
    }

    deleteConfirm(template: TemplateRef<any>, data: any) {
        this.question = Object.assign({}, data);
        this.modalRef = this.modalService.show(template);
    }

    confirm(): void {
        if (this.question) {
            this.questionService.deleteQuestion(this.question.id)
                .subscribe(
                    () => {
                        this.getAllQuestions(this.page);
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
        this.question = undefined;
        this.modalRef.hide();
    }

    close(): void {
        this.question = undefined;
        this.modalRef.hide();
    }

    search() {
        this.getAllQuestions(this.page);
    }

    searchCharacter() {
        this.itemsAsync = this.questionService.getAllQuestions(this.keyword, this.page, this.pageSize)
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
        this.getAllQuestions(this.page);
    }

    filterCourses(filterVal: any){
        if(filterVal !="0") this.keyword = filterVal;
        else this.keyword = '';
        this.getAllQuestions(this.page);
        this.keyword = '';
        
    }

}

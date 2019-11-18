import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Question } from 'src/app/models/question/question.model';
import { Router, ActivatedRoute } from '@angular/router';
import { QuestionService } from 'src/app/services/question.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { Observable } from 'rxjs';
import { CourseService } from 'src/app/services/course.service';

@Component({
    selector: 'app-edit-question',
    templateUrl: './edit-question.component.html'
})
export class EditQuestionComponent implements OnInit {

    editQuestionForm: FormGroup;
    question: Question;
    id: any;
    courses: Observable<any[]>;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private questionService: QuestionService,
        private courseService: CourseService,
        private toastr: ToastrService
    ) {
        this.editQuestionForm = this.fb.group({
            name: ['', Validators.required],
            courseId: ['', Validators.required],
            content: ['', Validators.required],
        });
    }

    ngOnInit() {
        this.courses = this.courseService.getAllCourseName();

        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.questionService.getQuestionById(this.id).subscribe(
                    result => {
                        this.question = result;
                        this.editQuestionForm.controls.name.setValue(result.name);
                        this.editQuestionForm.controls.courseId.setValue(result.courseId);
                        this.editQuestionForm.controls.content.setValue(result.content);
                    },
                    () => {
                        this.toastr.error(`Không tìm thấy câu hỏi này`);
                    });
            }
        });
    }

    editQuestion() {
        this.question = Object.assign({}, this.editQuestionForm.value);

        this.questionService.editQuestion(this.id, this.question).subscribe(
            () => {
                this.router.navigate(['/questions']).then(() => {
                    this.toastr.success('Cập nhật câu hỏi thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Cập nhật câu hỏi không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.editQuestionForm.controls; }
}

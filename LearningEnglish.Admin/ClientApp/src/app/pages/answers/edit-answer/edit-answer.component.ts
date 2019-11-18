import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Answer } from 'src/app/models/answer/answer.model';
import { Router, ActivatedRoute } from '@angular/router';
import { AnswerService } from 'src/app/services/answer.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { Observable } from 'rxjs';
import { QuestionService } from 'src/app/services/question.service';

@Component({
    selector: 'app-edit-answer',
    templateUrl: './edit-answer.component.html'
})
export class EditAnswerComponent implements OnInit {

    editAnswerForm: FormGroup;
    answer: Answer;
    id: any;
    questions: Observable<any[]>;
    corrects: any[] = [
        { key: false, value: ['Sai'] },
        { key: true, value: ['Đúng'] }
    ]

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private answerService: AnswerService,
        private questionService: QuestionService,
        private toastr: ToastrService
    ) {
        this.editAnswerForm = this.fb.group({
            questionId: ['', Validators.required],
            name: ['', Validators.required],
            content: ['', Validators.required],
            correctAnswer: ['', Validators.required],
        });
    }

    ngOnInit() {
        this.questions = this.questionService.getAllQuestionName();

        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.answerService.getAnswerById(this.id).subscribe(
                    result => {
                        this.answer = result;
                        this.editAnswerForm.controls.questionId.setValue(result.questionId);
                        this.editAnswerForm.controls.name.setValue(result.name);
                        this.editAnswerForm.controls.content.setValue(result.content);
                        this.editAnswerForm.controls.correctAnswer.setValue(result.correctAnswer);
                    },
                    () => {
                        this.toastr.error(`Không tìm thấy câu trả lời này`);
                    });
            }
        });
    }

    editAnswer() {
        this.answer = Object.assign({}, this.editAnswerForm.value);

        this.answerService.editAnswer(this.id, this.answer).subscribe(
            () => {
                this.router.navigate(['/answers']).then(() => {
                    this.toastr.success('Cập nhật câu trả lời thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Cập nhật câu trả lời không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.editAnswerForm.controls; }
}

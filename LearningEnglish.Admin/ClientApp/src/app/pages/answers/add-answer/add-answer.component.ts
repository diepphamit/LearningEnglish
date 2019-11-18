import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { AnswerService } from 'src/app/services/answer.service';
import { Observable } from 'rxjs';
import { AnswerAdd } from 'src/app/models/answer/answerAdd.model';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-add-answer',
  templateUrl: './add-answer.component.html'
})
export class AddAnswerComponent implements OnInit {

  addAnswerForm: FormGroup;
  answer: AnswerAdd;
  corrects: any[] = [
    { key: false, value: ['Sai'] },
    { key: true, value: ['Đúng'] }
  ]
  isTrue: boolean;

  questions: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private answerService: AnswerService,
    private questionService: QuestionService,
    private toastr: ToastrService
  ) {
    this.addAnswerForm = this.fb.group({
      questionId: ['', Validators.required],
      name: ['', Validators.required],
      content: ['', Validators.required], 
      correctAnswer: ['', Validators.required]   
    });
  }

  ngOnInit() {
    this.questions = this.questionService.getAllQuestionName();
  }
  addAnswer() {
    this.answer = Object.assign({}, this.addAnswerForm.value);
    this.answerService.createAnswer(this.answer).subscribe(
      () => {
        this.router.navigate(['/answers']).then(() => {
          this.toastr.success('Tạo câu trả lời thành công');
        });
      },
      (error: HttpErrorResponse) => {
        const errors = Utilities.getErrorResponses(error);

        if (!errors.length) {
          errors.push('Tạo câu trả lời không thành công!');
        }

        this.toastr.error(errors.join(','));
      }
    );
  }

  get f() { return this.addAnswerForm.controls; }
}

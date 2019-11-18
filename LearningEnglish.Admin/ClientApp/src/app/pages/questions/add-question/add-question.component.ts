import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { QuestionService } from 'src/app/services/question.service';
import { CourseService } from 'src/app/services/course.service';
import { Observable } from 'rxjs';
import { QuestionForAdd } from 'src/app/models/question/questionAdd.model';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html'
})
export class AddQuestionComponent implements OnInit {

  addQuestionForm: FormGroup;
  question: QuestionForAdd;

  courses: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private questionService: QuestionService,
    private courseService: CourseService,
    private toastr: ToastrService
  ) {
    this.addQuestionForm = this.fb.group({
      name: ['', Validators.required],
      courseId: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.courses = this.courseService.getAllCourseName();
  }
  addQuestion() {
    this.question = Object.assign({}, this.addQuestionForm.value);
    this.questionService.createQuestion(this.question).subscribe(
      () => {
        this.router.navigate(['/questions']).then(() => {
          this.toastr.success('Tạo câu hỏi thành công');
        });
      },
      (error: HttpErrorResponse) => {
        const errors = Utilities.getErrorResponses(error);

        if (!errors.length) {
          errors.push('Tạo câu hỏi không thành công!');
        }

        this.toastr.error(errors.join(','));
      }
    );
  }

  get f() { return this.addQuestionForm.controls; }
}

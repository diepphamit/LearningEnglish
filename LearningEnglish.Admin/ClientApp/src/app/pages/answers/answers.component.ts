import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Answer } from 'src/app/models/answer/answer.model';
import { Observable } from 'rxjs';
import { AnswerService } from 'src/app/services/answer.service';
import { QuestionService } from 'src/app/services/question.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html'
})
export class AnswersComponent implements OnInit {

  modalRef: BsModalRef;
  answer: Answer;
  keyword: string;
  page: number;
  pageSize: number;
  questions: Observable<any[]>;
  total: number;
  itemsAsync: Observable<any[]>;

  constructor(
    private answerService: AnswerService,
    private questionService: QuestionService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllAnswers(this.page);
    this.questions = this.questionService.getAllQuestionName();
  }

  getAllAnswers(page: number) {
    this.itemsAsync = this.answerService.getAllAnswers(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  add() {
    this.router.navigate(['/answers/add']);
  }

  edit(id: any) {
    this.router.navigate(['/answers/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.answer = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.answer) {
      this.answerService.deleteAnswer(this.answer.id)
        .subscribe(
          () => {
            this.getAllAnswers(this.page);
            this.toastr.success(`Xóa từ vựng thành công`);
          },
          (error: HttpErrorResponse) => {
            const errors = Utilities.getErrorResponses(error);

            if (!errors.length) {
              errors.push(`Xóa từ vựng không thành công!`);
            }

            this.toastr.error(errors.join(','));
          }
        );
    }
    this.answer = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.answer = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllAnswers(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllAnswers(this.page);
  }

  filterQuestions(filterVal: any) {
    if (filterVal != "0") this.keyword = filterVal;
    else this.keyword = '';
    this.getAllAnswers(this.page);
    this.keyword = '';

  }

}

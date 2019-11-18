import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Comment } from 'src/app/models/comment/comment.model';
import { Observable } from 'rxjs';
import { CommentService } from 'src/app/services/comment.service';
import { LessonService } from 'src/app/services/lesson.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html'
})
export class CommentsComponent implements OnInit {

  modalRef: BsModalRef;
  comment: Comment;
  keyword: string;
  page: number;
  pageSize: number;
  total: number;
  itemsAsync: Observable<any[]>;

  constructor(
    private commentService: CommentService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllComments(this.page);
  }

  getAllComments(page: number) {
    this.itemsAsync = this.commentService.getAllComments(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.comment = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.comment) {
      this.commentService.deleteComment(this.comment.id)
        .subscribe(
          () => {
            this.getAllComments(this.page);
            this.toastr.success(`Xóa từ binh luan công`);
          },
          (error: HttpErrorResponse) => {
            const errors = Utilities.getErrorResponses(error);

            if (!errors.length) {
              errors.push(`Xóa binh luan không thành công!`);
            }

            this.toastr.error(errors.join(','));
          }
        );
    }
    this.comment = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.comment = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllComments(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllComments(this.page);
  }

}

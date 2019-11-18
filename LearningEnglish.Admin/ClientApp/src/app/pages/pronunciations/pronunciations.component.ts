import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Pronunciation } from 'src/app/models/pronunciation/pronunciation.model';
import { Observable } from 'rxjs';
import { PronunciationService } from 'src/app/services/pronunciation.service';
import { LessonService } from 'src/app/services/lesson.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-pronunciations',
  templateUrl: './pronunciations.component.html'
})
export class PronunciationsComponent implements OnInit {

  modalRef: BsModalRef;
  pronunciation: Pronunciation;
  keyword: string;
  page: number;
  pageSize: number;
  lessons: Observable<any[]>;
  total: number;
  itemsAsync: Observable<any[]>;

  constructor(
    private pronunciationService: PronunciationService,
    private lessonService: LessonService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllPronunciations(this.page);
    this.lessons = this.lessonService.getAllLessonName();
  }

  getAllPronunciations(page: number) {
    this.itemsAsync = this.pronunciationService.getAllPronunciations(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  add() {
    this.router.navigate(['/pronunciations/add']);
  }

  edit(id: any) {
    this.router.navigate(['/pronunciations/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.pronunciation = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.pronunciation) {
      this.pronunciationService.deletePronunciation(this.pronunciation.id)
        .subscribe(
          () => {
            this.getAllPronunciations(this.page);
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
    this.pronunciation = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.pronunciation = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllPronunciations(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllPronunciations(this.page);
  }

  filterLessons(filterVal: any) {
    if (filterVal != "0") this.keyword = filterVal;
    else this.keyword = '';
    this.getAllPronunciations(this.page);
    this.keyword = '';

  }

}

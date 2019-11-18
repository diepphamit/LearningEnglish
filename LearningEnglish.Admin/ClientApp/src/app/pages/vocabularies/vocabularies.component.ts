import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Vocabulary } from 'src/app/models/vocabulary/vocabulary.model';
import { Observable } from 'rxjs';
import { VocabularyService } from 'src/app/services/vocabulary.service';
import { LessonService } from 'src/app/services/lesson.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-vocabularies',
  templateUrl: './vocabularies.component.html'
})
export class VocabulariesComponent implements OnInit {

  modalRef: BsModalRef;
  vocabulary: Vocabulary;
  keyword: string;
  page: number;
  pageSize: number;
  lessons: Observable<any[]>;
  total: number;
  itemsAsync: Observable<any[]>;

  constructor(
    private vocabularyService: VocabularyService,
    private lessonService: LessonService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllVocabularies(this.page);
    this.lessons = this.lessonService.getAllLessonName();
  }

  getAllVocabularies(page: number) {
    this.itemsAsync = this.vocabularyService.getAllVocabularies(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  add() {
    this.router.navigate(['/vocabularies/add']);
  }

  edit(id: any) {
    this.router.navigate(['/vocabularies/edit/' + id]);
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.vocabulary = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.vocabulary) {
      this.vocabularyService.deleteVocabulary(this.vocabulary.id)
        .subscribe(
          () => {
            this.getAllVocabularies(this.page);
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
    this.vocabulary = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.vocabulary = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllVocabularies(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllVocabularies(this.page);
  }

  filterLessons(filterVal: any) {
    if (filterVal != "0") this.keyword = filterVal;
    else this.keyword = '';
    this.getAllVocabularies(this.page);
    this.keyword = '';

  }

}

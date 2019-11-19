import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { VocabularyService } from 'src/app/services/vocabulary.service';
import { Observable } from 'rxjs';
import { VocabularyForAdd } from 'src/app/models/vocabulary/vocabularyAdd.model';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
  selector: 'app-add-vocabulary',
  templateUrl: './add-vocabulary.component.html'
})
export class AddVocabularyComponent implements OnInit {

  addVocabularyForm: FormGroup;
  vocabulary: VocabularyForAdd;

  lessons: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private vocabularyService: VocabularyService,
    private lessonService: LessonService,
    private toastr: ToastrService
  ) {
    this.addVocabularyForm = this.fb.group({
      lessonId: ['', Validators.required],
      phonetic: ['', Validators.required],
      video: ['', Validators.required],
      audio: ['', Validators.required],
      name: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.lessons = this.lessonService.getAllLessonName();
  }
  addVocabulary() {
    this.vocabulary = Object.assign({}, this.addVocabularyForm.value);
    this.vocabularyService.createVocabulary(this.vocabulary).subscribe(
      () => {
        this.router.navigate(['/vocabularies']).then(() => {
          this.toastr.success('Tạo từ vựng thành công');
        });
      },
      (error: HttpErrorResponse) => {
        const errors = Utilities.getErrorResponses(error);

        if (!errors.length) {
          errors.push('Tạo từ vựng không thành công!');
        }

        this.toastr.error(errors.join(','));
      }
    );
  }

  get f() { return this.addVocabularyForm.controls; }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { PronunciationService } from 'src/app/services/pronunciation.service';
import { Observable } from 'rxjs';
import { PronunciationForAdd } from 'src/app/models/pronunciation/pronunciationAdd.model';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
  selector: 'app-add-pronunciation',
  templateUrl: './add-pronunciation.component.html'
})
export class AddPronunciationComponent implements OnInit {

  addPronunciationForm: FormGroup;
  pronunciation: PronunciationForAdd;

  lessons: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private pronunciationService: PronunciationService,
    private lessonService: LessonService,
    private toastr: ToastrService
  ) {
    this.addPronunciationForm = this.fb.group({
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
  addPronunciation() {
    this.pronunciation = Object.assign({}, this.addPronunciationForm.value);
    this.pronunciationService.createPronunciation(this.pronunciation).subscribe(
      () => {
        this.router.navigate(['/pronunciations']).then(() => {
          this.toastr.success('Tạo phát âm nguyên âm thành công');
        });
      },
      (error: HttpErrorResponse) => {
        const errors = Utilities.getErrorResponses(error);

        if (!errors.length) {
          errors.push('Tạo phát âm nguyên âm không thành công!');
        }

        this.toastr.error(errors.join(','));
      }
    );
  }

  get f() { return this.addPronunciationForm.controls; }
}

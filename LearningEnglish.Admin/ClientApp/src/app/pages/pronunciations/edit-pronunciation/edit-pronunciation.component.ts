import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Pronunciation } from 'src/app/models/pronunciation/pronunciation.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PronunciationService } from 'src/app/services/pronunciation.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { Observable } from 'rxjs';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
    selector: 'app-edit-pronunciation',
    templateUrl: './edit-pronunciation.component.html'
})
export class EditPronunciationComponent implements OnInit {

    editPronunciationForm: FormGroup;
    pronunciation: Pronunciation;
    id: any;
    lessons: Observable<any[]>;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private pronunciationService: PronunciationService,
        private lessonService: LessonService,
        private toastr: ToastrService
    ) {
        this.editPronunciationForm = this.fb.group({
            lessonId: ['', Validators.required],
            phonetic: ['', Validators.required],
            video: ['', Validators.required],
            audio: ['', Validators.required],
        });
    }

    ngOnInit() {
        this.lessons = this.lessonService.getAllLessonName();

        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.pronunciationService.getPronunciationById(this.id).subscribe(
                    result => {
                        this.pronunciation = result;
                        this.editPronunciationForm.controls.lessonId.setValue(result.lessonId);
                        this.editPronunciationForm.controls.phonetic.setValue(result.phonetic);
                        this.editPronunciationForm.controls.video.setValue(result.video);
                        this.editPronunciationForm.controls.audio.setValue(result.audio);
                    },
                    () => {
                        this.toastr.error(`Không tìm thấy từ mới này`);
                    });
            }
        });
    }

    editPronunciation() {
        this.pronunciation = Object.assign({}, this.editPronunciationForm.value);

        this.pronunciationService.editPronunciation(this.id, this.pronunciation).subscribe(
            () => {
                this.router.navigate(['/pronunciations']).then(() => {
                    this.toastr.success('Cập nhật từ mới thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Cập nhật từ mới không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.editPronunciationForm.controls; }
}

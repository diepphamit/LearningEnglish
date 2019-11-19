import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Vocabulary } from 'src/app/models/vocabulary/vocabulary.model';
import { Router, ActivatedRoute } from '@angular/router';
import { VocabularyService } from 'src/app/services/vocabulary.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { Observable } from 'rxjs';
import { LessonService } from 'src/app/services/lesson.service';

@Component({
    selector: 'app-edit-vocabulary',
    templateUrl: './edit-vocabulary.component.html'
})
export class EditVocabularyComponent implements OnInit {

    editVocabularyForm: FormGroup;
    vocabulary: Vocabulary;
    id: any;
    lessons: Observable<any[]>;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private vocabularyService: VocabularyService,
        private lessonService: LessonService,
        private toastr: ToastrService
    ) {
        this.editVocabularyForm = this.fb.group({
            lessonId: ['', Validators.required],
            phonetic: ['', Validators.required],
            video: ['', Validators.required],
            audio: ['', Validators.required],
            name: ['', Validators.required],
        });
    }

    ngOnInit() {
        this.lessons = this.lessonService.getAllLessonName();

        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.vocabularyService.getVocabularyById(this.id).subscribe(
                    result => {
                        this.vocabulary = result;
                        this.editVocabularyForm.controls.lessonId.setValue(result.lessonId);
                        this.editVocabularyForm.controls.phonetic.setValue(result.phonetic);
                        this.editVocabularyForm.controls.video.setValue(result.video);
                        this.editVocabularyForm.controls.audio.setValue(result.audio);
                        this.editVocabularyForm.controls.name.setValue(result.name);
                    },
                    () => {
                        this.toastr.error(`Không tìm thấy từ mới này`);
                    });
            }
        });
    }

    editVocabulary() {
        this.vocabulary = Object.assign({}, this.editVocabularyForm.value);

        this.vocabularyService.editVocabulary(this.id, this.vocabulary).subscribe(
            () => {
                this.router.navigate(['/vocabularies']).then(() => {
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

    get f() { return this.editVocabularyForm.controls; }
}

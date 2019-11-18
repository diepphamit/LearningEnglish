import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Lesson } from 'src/app/models/lesson/lesson.model';
import { Router, ActivatedRoute } from '@angular/router';
import { LessonService } from 'src/app/services/lesson.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { Observable } from 'rxjs';
import { CourseService } from 'src/app/services/course.service';

@Component({
    selector: 'app-edit-lesson',
    templateUrl: './edit-lesson.component.html'
})
export class EditLessonComponent implements OnInit {

    editLessonForm: FormGroup;
    lesson: Lesson;
    id: any;
    courses: Observable<any[]>;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private lessonService: LessonService,
        private courseService: CourseService,
        private toastr: ToastrService
    ) {
        this.editLessonForm = this.fb.group({
            name: ['', Validators.required],
            courseId: ['', Validators.required],
            type: ['', Validators.required],
            introduce: ['', Validators.required],
            video: ['', Validators.required],
            tittle: ['', Validators.required]
        });
    }

    ngOnInit() {
        this.courses = this.courseService.getAllCourseName();

        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.lessonService.getLessonById(this.id).subscribe(
                    result => {
                        this.lesson = result;
                        this.editLessonForm.controls.name.setValue(result.name);
                        this.editLessonForm.controls.courseId.setValue(result.courseId);
                        this.editLessonForm.controls.type.setValue(result.type);
                        this.editLessonForm.controls.introduce.setValue(result.introduce);
                        this.editLessonForm.controls.video.setValue(result.video);
                        this.editLessonForm.controls.tittle.setValue(result.tittle);
                    },
                    () => {
                        this.toastr.error(`Không tìm thấy khóa học này`);
                    });
            }
        });
    }

    editLesson() {
        this.lesson = Object.assign({}, this.editLessonForm.value);

        this.lessonService.editLesson(this.id, this.lesson).subscribe(
            () => {
                this.router.navigate(['/lessons']).then(() => {
                    this.toastr.success('Cập nhật khóa học thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Cập nhật khóa học không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.editLessonForm.controls; }
}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Course } from 'src/app/models/course/course.model';
import { Router, ActivatedRoute } from '@angular/router';
import { CourseService } from 'src/app/services/course.service';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
    selector: 'app-edit-course',
    templateUrl: './edit-course.component.html'
})
export class EditCourseComponent implements OnInit {

    editCourseForm: FormGroup;
    course: Course;
    id: any;
    level: any[] = [
        { key: 1, value: 1 },
        { key: 2, value: 2 },
        { key: 3, value: 3 },
        { key: 4, value: 4 },
        { key: 5, value: 5 }
    ]

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private courseService: CourseService,
        private toastr: ToastrService
    ) {
        this.editCourseForm = this.fb.group({
            name: ['', Validators.required],
            introduce: ['', Validators.required],
            image: ['', Validators.required],
            levelClass: ['', Validators.required]
        });
    }


    ngOnInit() {
        this.route.params.subscribe(params => {
            this.id = params.id;
            if (this.id) {
                this.courseService.getCourseById(this.id).subscribe(
                    result => {
                        this.course = result;
                        this.editCourseForm.controls.name.setValue(result.name);
                        this.editCourseForm.controls.introduce.setValue(result.introduce);
                        this.editCourseForm.controls.image.setValue(result.image);
                        this.editCourseForm.controls.levelClass.setValue(result.levelClass);
                    },
                    error => {
                        this.toastr.error(`Không tìm thấy khóa học này`);
                    });
            }
        });
    }

    editCourse() {
        this.course = Object.assign({}, this.editCourseForm.value);

        this.courseService.editCourse(this.id, this.course).subscribe(
            () => {
                this.router.navigate(['/courses']).then(() => {
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

    get f() { return this.editCourseForm.controls; }
}

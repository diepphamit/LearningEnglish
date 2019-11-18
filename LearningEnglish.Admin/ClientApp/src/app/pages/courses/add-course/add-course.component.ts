import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseService } from 'src/app/services/course.service';
import { Course } from 'src/app/models/course/course.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
    selector: 'app-add-course',
    templateUrl: './add-course.component.html'
})
export class AddCourseComponent implements OnInit {
    
    addCourseForm: FormGroup;
    course: Course;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private courseService: CourseService,
        private toastr: ToastrService
    ) {
        this.addCourseForm = this.fb.group({
            name: ['', Validators.required],
            introduce: ['', Validators.required],
            image: ['', Validators.required]
        });
     }

    ngOnInit() {
    }

    addCourse() {
        this.course = Object.assign({}, this.addCourseForm.value);

        this.courseService.createCourse(this.course).subscribe(
            () => {
                this.router.navigate(['/courses']).then(() => {
                    this.toastr.success('Tạo khóa học thành công');
                });
            },
            (error: HttpErrorResponse) => {
                const errors = Utilities.getErrorResponses(error);

                if (!errors.length) {
                    errors.push('Tạo khóa học không thành công!');
                }

                this.toastr.error(errors.join(','));
            }
        );
    }

    get f() { return this.addCourseForm.controls; }

    
}

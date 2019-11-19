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
        private courseService: CourseService,
        private toastr: ToastrService
    ) {
        this.addCourseForm = this.fb.group({
            name: ['', Validators.required],
            introduce: ['', Validators.required],
            image: ['', Validators.required],
            levelClass: ['', Validators.required]
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

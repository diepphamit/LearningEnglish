import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { LessonService } from 'src/app/services/lesson.service';
import { CourseService } from 'src/app/services/course.service';
import { Observable } from 'rxjs';
import { LessonForAdd } from 'src/app/models/lesson/lessonAdd.model';

@Component({
  selector: 'app-add-lesson',
  templateUrl: './add-lesson.component.html'
})
export class AddLessonComponent implements OnInit {

  addLessonForm: FormGroup;
  lesson: LessonForAdd;

  courses: Observable<any[]>;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private lessonService: LessonService,
    private courseService: CourseService,
    private toastr: ToastrService
  ) {
    this.addLessonForm = this.fb.group({
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
  }
  addLesson() {
    this.lesson = Object.assign({}, this.addLessonForm.value);
    this.lessonService.createLesson(this.lesson).subscribe(
      () => {
        this.router.navigate(['/lessons']).then(() => {
          this.toastr.success('Tạo bài học thành công');
        });
      },
      (error: HttpErrorResponse) => {
        const errors = Utilities.getErrorResponses(error);

        if (!errors.length) {
          errors.push('Tạo bài học không thành công!');
        }

        this.toastr.error(errors.join(','));
      }
    );
  }

  get f() { return this.addLessonForm.controls; }
}

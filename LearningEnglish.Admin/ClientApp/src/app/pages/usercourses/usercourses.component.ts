import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserCourse } from 'src/app/models/usercourse/usercourse.model';
import { Observable } from 'rxjs';
import { UserCourseService } from 'src/app/services/usercourse.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';
import { CourseService } from 'src/app/services/course.service';
import { Course } from 'src/app/models/course/course.model';

@Component({
  selector: 'app-usercourses',
  templateUrl: './usercourses.component.html'
})
export class UsercoursesComponent implements OnInit {

  modalRef: BsModalRef;
  usercourse: UserCourse;
  keyword: string;
  page: number;
  pageSize: number;
  total: number;
  courses: Observable<Course[]>;
  itemsAsync: Observable<any[]>;

  constructor(
    private usercourseService: UserCourseService,
    private courseService: CourseService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllUsercourses(this.page);
    this.courses = this.courseService.getAllCourseName();
  }

  getAllUsercourses(page: number) {
    this.itemsAsync = this.usercourseService.getAllUserCourses(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.usercourse = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.usercourse) {
      this.usercourseService.deleteUserCourse(this.usercourse.id)
        .subscribe(
          () => {
            this.getAllUsercourses(this.page);
            this.toastr.success(`Xóa thành công công`);
          },
          (error: HttpErrorResponse) => {
            const errors = Utilities.getErrorResponses(error);

            if (!errors.length) {
              errors.push(`Xóa không thành công!`);
            }

            this.toastr.error(errors.join(','));
          }
        );
    }
    this.usercourse = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.usercourse = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllUsercourses(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllUsercourses(this.page);
  }
  filterCourses(filterVal: any){
    if(filterVal !="0")  this.courseService.getCourseById(+filterVal).subscribe( x => {
      this.keyword = x.name ;
      this.getAllUsercourses(this.page);
      this.keyword = '';});
    else this.getAllUsercourses(this.page);
    
}

}

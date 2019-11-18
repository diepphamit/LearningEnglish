import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Achievement } from 'src/app/models/achievement/achievement.model';
import { Observable } from 'rxjs';
import { AchievementService } from 'src/app/services/achievement.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-achievements',
  templateUrl: './achievements.component.html'
})
export class AchievementsComponent implements OnInit {

  modalRef: BsModalRef;
  achievement: Achievement;
  keyword: string;
  page: number;
  pageSize: number;
  total: number;
  itemsAsync: Observable<any[]>;

  constructor(
    private achievementService: AchievementService,
    private router: Router,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.keyword = '';
    this.page = 1;
    this.pageSize = 10;
    this.getAllAchievements(this.page);
  }

  getAllAchievements(page: number) {
    this.itemsAsync = this.achievementService.getAllAchievements(this.keyword, page, this.pageSize)
      .pipe(
        tap(response => {
          this.total = response.total;
          this.page = page;
        }),
        map(response => response.items)
      );
  }

  deleteConfirm(template: TemplateRef<any>, data: any) {
    this.achievement = Object.assign({}, data);
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    if (this.achievement) {
      this.achievementService.deleteAchievement(this.achievement.id)
        .subscribe(
          () => {
            this.getAllAchievements(this.page);
            this.toastr.success(`Xóa từ kết quả thi công`);
          },
          (error: HttpErrorResponse) => {
            const errors = Utilities.getErrorResponses(error);

            if (!errors.length) {
              errors.push(`Xóa kết quả thi không thành công!`);
            }

            this.toastr.error(errors.join(','));
          }
        );
    }
    this.achievement = undefined;
    this.modalRef.hide();
  }

  close(): void {
    this.achievement = undefined;
    this.modalRef.hide();
  }

  search() {
    this.getAllAchievements(this.page);
  }

  refresh() {
    this.keyword = '';
    this.getAllAchievements(this.page);
  }

}

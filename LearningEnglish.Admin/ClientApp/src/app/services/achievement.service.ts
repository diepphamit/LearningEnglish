import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AchievementService {
    baseUrl = environment.apiUrl + 'Achievements';

    constructor(private http: HttpClient) {
    }

    getAllAchievements(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAchievementById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    deleteAchievement(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

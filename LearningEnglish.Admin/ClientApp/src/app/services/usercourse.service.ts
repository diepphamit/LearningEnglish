import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class UserCourseService {
    baseUrl = environment.apiUrl + 'UserCourses';

    constructor(private http: HttpClient) {
    }

    getAllUserCourses(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getUserCourseById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    deleteUserCourse(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

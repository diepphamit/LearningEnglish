import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class LessonService {
    baseUrl = environment.apiUrl + 'Lessons';

    constructor(private http: HttpClient) {
    }

    getAllLessons(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAllLessonName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllLessonName');
    }

    getLessonById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createLesson(lesson: any) {
        return this.http.post(this.baseUrl, lesson);
    }

    editLesson(id: any, lesson: any) {
        return this.http.put(`${this.baseUrl}/${id}`, lesson);
    }

    deleteLesson(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

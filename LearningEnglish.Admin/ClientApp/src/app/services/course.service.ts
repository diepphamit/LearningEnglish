import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CourseService {
    baseUrl = environment.apiUrl + 'Courses';

    constructor(private http: HttpClient) {
    }

    getAllCourses(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }
    getAllCourseName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllCourseName');
    }

    getCourseById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createCourse(course: any) {
        return this.http.post(this.baseUrl, course);
    }

    editCourse(id: any, course: any) {
        return this.http.put(`${this.baseUrl}/${id}`, course);
    }

    deleteCourse(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

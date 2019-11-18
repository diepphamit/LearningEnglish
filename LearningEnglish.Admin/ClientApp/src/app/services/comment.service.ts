import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CommentService {
    baseUrl = environment.apiUrl + 'Comments';

    constructor(private http: HttpClient) {
    }

    getAllComments(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getCommentById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    deleteComment(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

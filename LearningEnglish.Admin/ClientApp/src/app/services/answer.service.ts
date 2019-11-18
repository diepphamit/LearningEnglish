import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AnswerService {
    baseUrl = environment.apiUrl + 'Answers';

    constructor(private http: HttpClient) {
    }

    getAllAnswers(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAllAnswerName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllAnswerName');
    }

    getAnswerById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createAnswer(answer: any) {
        return this.http.post(this.baseUrl, answer);
    }

    editAnswer(id: any, answer: any) {
        return this.http.put(`${this.baseUrl}/${id}`, answer);
    }

    deleteAnswer(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

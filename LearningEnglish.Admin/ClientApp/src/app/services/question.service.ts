import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class QuestionService {
    baseUrl = environment.apiUrl + 'Questions';

    constructor(private http: HttpClient) {
    }

    getAllQuestions(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAllQuestionName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllQuestionName');
    }

    getQuestionById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createQuestion(question: any) {
        return this.http.post(this.baseUrl, question);
    }

    editQuestion(id: any, question: any) {
        return this.http.put(`${this.baseUrl}/${id}`, question);
    }

    deleteQuestion(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

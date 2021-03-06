import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class PronunciationService {
    baseUrl = environment.apiUrl + 'Pronunciations';

    constructor(private http: HttpClient) {
    }

    getAllPronunciations(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAllPronunciationName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllPronunciationName');
    }

    getPronunciationById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createPronunciation(pronunciation: any) {
        return this.http.post(this.baseUrl, pronunciation);
    }

    editPronunciation(id: any, pronunciation: any) {
        return this.http.put(`${this.baseUrl}/${id}`, pronunciation);
    }

    deletePronunciation(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

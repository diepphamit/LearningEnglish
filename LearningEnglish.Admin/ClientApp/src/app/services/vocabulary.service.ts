import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class VocabularyService {
    baseUrl = environment.apiUrl + 'Vocabularies';

    constructor(private http: HttpClient) {
    }

    getAllVocabularies(keyword: string, page: number, pageSize: number): Observable<any> {
        return this.http.get(`${this.baseUrl}?keyword=${keyword}&page=${page}&pageSize=${pageSize}`);
    }

    getAllVocabularyName(): Observable<any> {
        return this.http.get(this.baseUrl + '/GetAllVocabularyName');
    }

    getVocabularyById(id: any): Observable<any> {
        return this.http.get(`${this.baseUrl}/${id}`);
    }

    createVocabulary(lesson: any) {
        return this.http.post(this.baseUrl, lesson);
    }

    editVocabulary(id: any, lesson: any) {
        return this.http.put(`${this.baseUrl}/${id}`, lesson);
    }

    deleteVocabulary(id: any) {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}

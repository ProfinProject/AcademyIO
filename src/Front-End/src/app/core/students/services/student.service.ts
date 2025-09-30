import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StudentRegistration } from '../models/student.interfaces';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'http://localhost:7234/api/Students';

  constructor(private http: HttpClient) { }

  getMyRegistrations(): Observable<StudentRegistration[]> {
    return this.http.get<StudentRegistration[]>(`${this.apiUrl}/get-registration`);
  }
}

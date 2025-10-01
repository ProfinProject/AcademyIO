import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course.interface';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CourseService {
    private apiUrl = `${environment.apiUrlv1}Course`;

  constructor(private http: HttpClient) { }

  /**
   * Busca a lista de cursos do backend.
   */
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.apiUrl}`);
  }

  /**
   * Exclui um curso pelo ID.
   */
  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove?id=${id}`);
  }
}

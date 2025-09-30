import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course.interface';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  // Ajuste a URL base da sua API se for diferente
  private apiUrl = 'https://localhost:7283/api/Course';

  constructor(private http: HttpClient) { }

  /**
   * Busca a lista de cursos do backend.
   */
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.apiUrl}/courses`);
  }

  /**
   * Exclui um curso pelo ID.
   */
  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove-course?id=${id}`);
  }
}

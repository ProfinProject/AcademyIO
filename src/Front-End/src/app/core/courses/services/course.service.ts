import { Inject, PLATFORM_ID, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Course } from '../models/course.interface';
import { CreateLessonDto, Lesson } from '../models/lesson.interface';
import { BaseService } from '../../../services/base.service';

@Injectable({
  providedIn: 'root'
})
export class CourseService extends BaseService {
  private apiUrl = 'https://localhost:7283/api/Course';

    constructor(
            private http: HttpClient,
            @Inject(PLATFORM_ID) platformId: Object
        ) {
            super(platformId); // ✅ Passando corretamente
        }

  /**
   * Busca a lista de cursos do backend.
   */
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.apiUrl}/courses`).pipe(
      catchError(this.handleError)
    );
  }

  /**
   * Exclui um curso pelo ID.
   */
  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove-course?id=${id}`, this.getAuthHeader()).pipe(catchError(this.handleError));
  }

  /**
   * Cria um novo curso no backend.
   */
  createCourse(courseData: Omit<Course, 'id'>): Observable<Course> {
    return this.http.post<Course>(`${this.apiUrl}/create-course`, courseData, this.getAuthHeaderJson()).pipe(catchError(this.handleError));
  }

  /**
   * Cria uma nova aula associada a um curso.
   */
  createLesson(lessonData: CreateLessonDto): Observable<Lesson> {
    return this.http.post<Lesson>(`${this.apiUrl}/create-lesson`, lessonData, this.getAuthHeaderJson()).pipe(catchError(this.handleError));
  }

  /**
   * Busca as aulas de um curso específico.
   */
  getLessonsByCourse(courseId: string): Observable<Lesson[]> {
    return this.http.get<Lesson[]>(`${this.apiUrl}/lesson-course?courseId=${courseId}`).pipe(catchError(this.handleError));
  }

  /**
   * Busca um curso específico pelo ID.
   */
  getCourseById(courseId: string): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/course?id=${courseId}`).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Ocorreu um erro desconhecido.';
    if (error.error instanceof ErrorEvent) {
      // Erro do lado do cliente
      errorMessage = `Erro: ${error.error.message}`;
    } else {
      // O backend retornou um código de erro.
      errorMessage = `Erro do servidor: ${error.status}, mensagem: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error('Falha na operação do curso. Por favor, tente novamente mais tarde.'));
  }
}

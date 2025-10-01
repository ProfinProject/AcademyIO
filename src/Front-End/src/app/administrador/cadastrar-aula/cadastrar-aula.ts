import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CourseService } from '../../core/courses/services/course.service';
import { CreateLessonDto, Lesson } from '../../core/courses/models/lesson.interface';
import { Course } from '../../core/courses/models/course.interface';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cadastrar-aula',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './cadastrar-aula.html',
  styleUrls: ['./cadastrar-aula.css'],
})
export class CadastrarAula implements OnInit {
  lesson: CreateLessonDto = {
    name: '',
    subject: '',
    totalHours: 0,
    courseId: '',
  };
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  courses$!: Observable<Course[]>;

  constructor(
    private courseService: CourseService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.courses$ = this.courseService.getCourses();
  }

  onSubmit(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.courseService.createLesson(this.lesson).subscribe({
      next: (createdLesson) => {
        this.isLoading = false;
        this.successMessage = `Aula cadastrada com sucesso!`;
        // Limpa o formulário, exceto o curso selecionado, para facilitar o cadastro de múltiplas aulas.
        this.lesson.name = '';
        this.lesson.subject = '';
        this.lesson.totalHours = 0;
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.message || 'Falha ao cadastrar a aula. Verifique os dados e tente novamente.';
        console.error('Erro ao criar aula:', err);
      },
    });
  }
}

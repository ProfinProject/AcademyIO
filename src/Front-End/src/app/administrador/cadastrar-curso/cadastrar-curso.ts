import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CourseService } from '../../core/courses/services/course.service';
import { Course } from '../../core/courses/models/course.interface';

@Component({
  selector: 'app-cadastrar-curso',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './cadastrar-curso.html',
  styleUrls: ['./cadastrar-curso.css']
})
export class CadastrarCurso {
  course: Omit<Course, 'id'> = {
    name: '',
    description: '',
    price: 0
  };
  isLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private courseService: CourseService, private router: Router) { }

  onSubmit(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.courseService.createCourse(this.course).subscribe({
      next: (createdCourse) => {
        this.isLoading = false;
        this.successMessage = `Curso cadastrado com sucesso!`;
        setTimeout(() => this.router.navigate(['/painel-administrador']), 2000);
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.message || 'Falha ao cadastrar o curso. Verifique os dados e tente novamente.';
        console.error('Erro ao criar curso:', err);
      }
    });
  }
}

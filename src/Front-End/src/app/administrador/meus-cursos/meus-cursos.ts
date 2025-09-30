import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { StudentService } from '../../core/students/services/student.service';
import { StudentRegistration } from '../../core/students/models/student.interfaces';

@Component({
  selector: 'app-meus-cursos',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './meus-cursos.html',
  styleUrls: ['./meus-cursos.css']
})
export class MeusCursos implements OnInit {
  myCourses$!: Observable<StudentRegistration[]>;

  constructor(private studentService: StudentService) { }

  ngOnInit(): void {
    this.loadMyCourses();
  }

  loadMyCourses(): void {
    this.myCourses$ = this.studentService.getMyRegistrations();
  }
}

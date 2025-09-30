import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Course } from '../../core/courses/models/course.interface';
import { CourseService } from '../../core/courses/services/course.service';

@Component({
  selector: 'app-cursos',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './cursos.html',
  styleUrls: ['./cursos.css']
})
export class Cursos implements OnInit {

  courses$!: Observable<Course[]>;

  constructor(private courseService: CourseService) { }

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    this.courses$ = this.courseService.getCourses();
  }
}

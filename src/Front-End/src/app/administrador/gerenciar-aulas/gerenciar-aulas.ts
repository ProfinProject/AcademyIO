import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { CourseService } from '../../core/courses/services/course.service';
import { Lesson } from '../../core/courses/models/lesson.interface';
import { Course } from '../../core/courses/models/course.interface';

@Component({
  selector: 'app-gerenciar-aulas',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './gerenciar-aulas.html',
  // styleUrls: ['./gerenciar-aulas.css'] // Se você tiver um arquivo CSS
})
export class GerenciarAulas implements OnInit {
  lessons$!: Observable<Lesson[]>;
  course$!: Observable<Course>;
  courseId!: string;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const courseId = this.route.snapshot.paramMap.get('courseId');
    if (courseId) {
      this.courseId = courseId;
      this.lessons$ = this.courseService.getLessonsByCourse(this.courseId);
      this.course$ = this.courseService.getCourseById(this.courseId);
    } else {
      console.error('Course ID is missing');
    }
  }
}

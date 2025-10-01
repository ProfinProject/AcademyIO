export interface Lesson {
  id: string;
  name: string;
  subject: string;
  /**
   * Duração da aula em horas.
   */
  totalHours: number;
  courseId: string;
}

export type CreateLessonDto = Omit<Lesson, 'id'>;

import { Course } from '../../courses/models/course.interface';

/**
 * Representa a matrícula de um estudante em um curso.
 * Por enquanto, assume-se que a API retorna os mesmos dados de um curso.
 * Esta interface pode ser expandida se a API retornar dados específicos da matrícula.
 */
export type StudentRegistration = Course;

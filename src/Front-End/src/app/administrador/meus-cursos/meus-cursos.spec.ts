import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusCursos } from './meus-cursos';

describe('MeusCursos', () => {
  let component: MeusCursos;
  let fixture: ComponentFixture<MeusCursos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeusCursos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MeusCursos);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

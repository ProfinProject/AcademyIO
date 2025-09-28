import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarCurso } from './cadastrar-curso';

describe('CadastrarCurso', () => {
  let component: CadastrarCurso;
  let fixture: ComponentFixture<CadastrarCurso>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastrarCurso]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastrarCurso);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

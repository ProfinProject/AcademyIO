import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarAula } from './cadastrar-aula';

describe('CadastrarAula', () => {
  let component: CadastrarAula;
  let fixture: ComponentFixture<CadastrarAula>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastrarAula]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastrarAula);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

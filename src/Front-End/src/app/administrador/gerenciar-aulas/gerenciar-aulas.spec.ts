import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GerenciarAulas } from './gerenciar-aulas';

describe('GerenciarAulas', () => {
  let component: GerenciarAulas;
  let fixture: ComponentFixture<GerenciarAulas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GerenciarAulas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GerenciarAulas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

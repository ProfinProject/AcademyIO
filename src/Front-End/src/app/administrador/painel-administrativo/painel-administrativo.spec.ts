import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelAdministrativo } from './painel-administrativo';

describe('PainelAdministrativo', () => {
  let component: PainelAdministrativo;
  let fixture: ComponentFixture<PainelAdministrativo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelAdministrativo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelAdministrativo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Ementa } from './ementa';

describe('Ementa', () => {
  let component: Ementa;
  let fixture: ComponentFixture<Ementa>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Ementa]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Ementa);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

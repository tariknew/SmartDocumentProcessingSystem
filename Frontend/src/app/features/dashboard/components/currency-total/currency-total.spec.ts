import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrencyTotal } from './currency-total';

describe('CurrencyTotal', () => {
  let component: CurrencyTotal;
  let fixture: ComponentFixture<CurrencyTotal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CurrencyTotal],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrencyTotal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

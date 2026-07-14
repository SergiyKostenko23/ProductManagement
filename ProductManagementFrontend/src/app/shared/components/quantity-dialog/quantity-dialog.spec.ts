import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuantityDialog } from './quantity-dialog';

describe('QuantityDialog', () => {
  let component: QuantityDialog;
  let fixture: ComponentFixture<QuantityDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuantityDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuantityDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectCustomComponent } from './select-custom.component';

describe('SelectCustomComponent', () => {
  let component: SelectCustomComponent;
  let fixture: ComponentFixture<SelectCustomComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectCustomComponent]
    });
    fixture = TestBed.createComponent(SelectCustomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

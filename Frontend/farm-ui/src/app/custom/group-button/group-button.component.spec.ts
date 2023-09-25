import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupButtonComponent } from './group-button.component';

describe('GroupButtonComponent', () => {
  let component: GroupButtonComponent;
  let fixture: ComponentFixture<GroupButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GroupButtonComponent]
    });
    fixture = TestBed.createComponent(GroupButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

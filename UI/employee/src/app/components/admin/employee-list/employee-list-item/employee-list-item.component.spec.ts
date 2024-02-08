import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeListItemComponent } from './employee-list-item.component';

describe('EmployeeListItemComponent', () => {
  let component: EmployeeListItemComponent;
  let fixture: ComponentFixture<EmployeeListItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeListItemComponent]
    });
    fixture = TestBed.createComponent(EmployeeListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

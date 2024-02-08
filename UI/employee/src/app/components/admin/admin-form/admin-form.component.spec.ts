import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFormComponent } from './admin-form.component';

describe('AdminFormComponent', () => {
  let component: AdminFormComponent;
  let fixture: ComponentFixture<AdminFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminFormComponent]
    });
    fixture = TestBed.createComponent(AdminFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

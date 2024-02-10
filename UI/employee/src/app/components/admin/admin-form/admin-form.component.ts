import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoleType } from 'src/app/models/RoleType';

@Component({
  selector: 'app-admin-form',
  templateUrl: './admin-form.component.html',
  styleUrls: ['./admin-form.component.css']
})
export class AdminFormComponent implements OnInit {
  registrationForm!: FormGroup;
  roleType = RoleType;
 
  
  constructor(private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      salary: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
      joinDate: ['', Validators.required],
      address: ['', Validators.required],
      telephone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      roleType: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Form Submitted', this.registrationForm.value);
      // Here you can implement your form submission logic, e.g., sending data to a server
    } else {
      console.log('Form is not valid', this.registrationForm.value);
    }
  }
}

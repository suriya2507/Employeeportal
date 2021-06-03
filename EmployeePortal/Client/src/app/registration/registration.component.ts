import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

// import custom validator to validate that password and confirm password fields match
import {Task} from "protractor/built/taskScheduler";
import { UserService } from '../user.service';
import { MustMatch } from '../customvalidators/must-match.validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  public httpClient;

  constructor(http: HttpClient,  private formBuild: FormBuilder, private userService: UserService)
  {
    this.httpClient = http;
  }

  ngOnInit() {
    this.registerForm = this.formBuild.group({
      //title: ['', Validators.required],
      title: ['', Validators.nullValidator],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      // validates date format yyyy-mm-dd
      dob: ['', [Validators.required, Validators.pattern(/^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue]
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  async onSubmit() {

    this.submitted = true;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }
    let registerRequestModel = {
      Email: this.registerForm.value.email,
      Password: this.registerForm.value.password,
      FirstName: this.registerForm.value.firstName,
      LastName: this.registerForm.value.lastName,
      DOB:this.registerForm.value.dob
    }

    if(!window.sessionStorage.getItem("jwt_token"))
    {
      let token = await this.userService.registerUser(registerRequestModel).toPromise();
      window.sessionStorage.setItem("jwt_token", token.Token);       
    }
    
    
    // this.httpClient.post(this.bUrl + 'registration', this.registerForm.value).subscribe(result => {
    //   this.response = result;
    // }, error => console.error(error));  


  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }
}


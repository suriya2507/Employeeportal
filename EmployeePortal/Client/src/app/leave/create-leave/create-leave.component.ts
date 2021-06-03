import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/user.service';

@Component({
  selector: 'app-create-leave',
  templateUrl: './create-leave.component.html',
  styleUrls: ['./create-leave.component.css']
})
export class CreateLeaveComponent implements OnInit {


  leaveForm: FormGroup;
  submitted = false;
  hide = true;
  
  
  constructor(
    private formBuild: FormBuilder,
    private router: Router,
    private activeroute: ActivatedRoute,
    private userService:UserService
  ) {}

  ngOnInit() {
    this.leaveForm = 
this.formBuild.group
({
      From: ['', Validators.required],
      To: ['', Validators.required],
      LeaveUserId: ['', Validators.required],
      Message: ['', Validators.required],

    });
  }

  get f() {
    return this.leaveForm.controls;
  }

  async onSubmit() {
    this.submitted = true;
    if (this.leaveForm.invalid) {
      return;
    } else {
      console.log(this.leaveForm.value);
      await this.userService.leave({from:this.leaveForm.value.From,to:this.leaveForm.value.To,message:this.leaveForm.value.Message,leaveUserId:this.leaveForm.value.LeaveUserId});

    }
  }

  onReset() {
    this.submitted = false;
    this.leaveForm.reset();
  }

}

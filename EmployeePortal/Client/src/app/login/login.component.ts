import {Component, OnInit, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {UserService} from '../user.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    submitted = false;
    hide = true;


    constructor(private formBuild: FormBuilder, private userService: UserService, private router : Router,private activeRoute:ActivatedRoute) {
		

    }

    ngOnInit() {
        this.loginForm =

            this.formBuild.group
            ({
                userName: ['', Validators.required],
                password: ['', [Validators.required, Validators.minLength(6)]],
            });
    }

    get f() {
        return this.loginForm.controls;
    }

    async onSubmit() {
        this.submitted = true;
        if (this.loginForm.invalid) {
            return;
        }
        else{
            await this.userService.login({login: this.loginForm.value.userName, password: this.loginForm.value.password});
		
		    this.router.navigateByUrl('/employee-details');
        }
    }


    onReset() {
        this.submitted = false;
        this.loginForm.reset();
    }
    goRegister() {
        return this.router.navigate(['/register'], {
          relativeTo: this.activeRoute,
        });
      }


}

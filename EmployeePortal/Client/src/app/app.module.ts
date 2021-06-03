import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {Router, RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LoginComponent} from './login/login.component';
import {MaterialModule} from './materialModule';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AuthenticationInterceptor} from './authenticationInterceptor';
import { CreateLeaveComponent } from './leave/create-leave/create-leave.component';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';
import { RegistrationComponent } from './registration/registration.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        LoginComponent,
        CreateLeaveComponent,
        EmployeeDetailComponent,
        RegistrationComponent
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        ReactiveFormsModule,
        MaterialModule,
        
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'login', component: LoginComponent},
            {path:'register',component:RegistrationComponent},
            {path: 'home', component: HomeComponent},
            {path: 'applyleave', component: CreateLeaveComponent},
            {path: 'employee-details', component: EmployeeDetailComponent},

        ]),
        BrowserAnimationsModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useFactory: function (router: Router) {
                return new AuthenticationInterceptor(router);
            },
            multi: true,
            deps: [Router]
        },
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}

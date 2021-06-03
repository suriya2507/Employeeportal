import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../environments/environment';
import {LoginRequestModel, LoginResponseModel} from './login/loginModels';
import { RegistrationRequestModel, RegistrationResponseModel } from './models/registrationModel/RegistrationRequestModel';
import { LeaveRequestModel, LeaveResponseModel } from './models/leaveRequestModel';

export const TOKEN_STORAGE_KEY = 'token';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private REGISTRAtION_ROUTE = environment.apiUrl + '/api/user';
    private LOGIN_ROUTE = environment.apiUrl + '/api/user/login';
    private LEAVE_ROUTE=environment.apiUrl+'/leave';

    constructor(private httpClient: HttpClient) {
    }

    public registerUser(model: RegistrationRequestModel): Observable<RegistrationResponseModel> {
        return this.httpClient.post<RegistrationResponseModel>(this.REGISTRAtION_ROUTE, model,);
    }

    public async login(model: LoginRequestModel) {
        const response = await this.httpClient.post<LoginResponseModel>(this.LOGIN_ROUTE, model)
            .toPromise();
        if (response && response.token) {
            localStorage.setItem(TOKEN_STORAGE_KEY, response.token);
        }
    }
    public async leave(model: LeaveRequestModel) {
        const response = await this.httpClient.post<LeaveResponseModel>(this.LEAVE_ROUTE, model)
            .toPromise();
        if (response && response.token) {
            console.warn(response.from)
            localStorage.setItem(TOKEN_STORAGE_KEY, response.token);
        }
    }


    // public async SomeAction(): void {
    //     const token = localStorage.getItem(TOKEN_STORAGE_KEY);
    //     this.httpClient.get(this.LOGIN_ROUTE, {
    //         headers: {
    //             'Authorization': `bearer ${token}`
    //         }
    //     });
    // }
}

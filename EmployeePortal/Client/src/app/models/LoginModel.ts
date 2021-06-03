export interface LoginRequestModel {
    userName: string;
    password: string;
}

export interface LoginResponseModel {
    FirstName:string,
    LastName:string,
    Email:string,
    DOB:string,

    token: string;
}
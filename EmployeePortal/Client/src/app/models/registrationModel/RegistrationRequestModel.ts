export interface RegistrationRequestModel {
    Email: string;
  Password: string;
  LastName: string;
  FirstName: string;
  DOB:string;

}

export interface RegistrationResponseModel {
    Email: string;
    Token: string;
}
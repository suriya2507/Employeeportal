export interface LeaveRequestModel {
    from: string;
    to: string;
    message:Text;
    leaveUserId:string;
}
export interface LeaveResponseModel {
    from: string;
    to: string;
    message:Text;
    leaveUserId:string;

    token:string;
}
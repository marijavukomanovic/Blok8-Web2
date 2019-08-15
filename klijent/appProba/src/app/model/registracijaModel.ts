import { PassengerType } from 'src/app/model/enums';

export class RegistrationModel{
    Name : string;
    LastName : string;
    UserName : string;
    Email : string;
    Address : string;
    BirthdayDate : string;
    PassengerType : PassengerType;
    Password : string;
    ConfirmPassword : string;
}
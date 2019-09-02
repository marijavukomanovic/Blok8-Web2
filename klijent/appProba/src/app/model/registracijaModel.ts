import { PassengerTypeEnum } from 'src/app/model/enums';

export class RegistracijaModel{
    Name : string;
    LastName : string;
    UserName : string;
    Email : string;
    Address : string;
    BirthdayDate : string;
    PassengerType : number;
    Password : string;
    ConfirmPassword : string;
    Document : string;
}

export class InfoModel{
    Name : string;
    LastName : string;
    UserName : string;
    Email : string;
    Address : string;
    BirthdayDate : string;
    PassengerType : number;
    Document : string;
    StatusVerifikacije: string;
}
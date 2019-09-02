import { RouteType } from './enums';
import { ThrowStmt } from '@angular/compiler';

export class LinijaModel{
    Id : number;
    RouteNumber : number;
    RouteType : RouteType;
}

export class Line{
    LineId:string;
    LineType:number;
    Description:string;
    Color:string;
}

export class StanicaModel
{
    Name :string;
    Address :string;
    Line: string;
    X:number;
    Y:number;
}

export class StanicaModelirana
{
    Name :string;
    Address :string;
    Line: string;
}


export class Station{
Name:string;
Address:string;
XCoordinate:number;
YCoordinate:number;
constructor (private name:string,private address:string,private xCoordinate:number,private yCoordinate:number ) {
    this.Name=name;
    this.Address=address;
    this.XCoordinate=xCoordinate;
    this.YCoordinate=yCoordinate;
    
}
}
export class LineStation{
    LineId:string;
    LineType:string;
    Description:string;
    Color:string;
    Stations:Array<Station>;
    constructor(private lineid:string, private linetype:string,private description:string ,private color:string)
    {
        this.LineId=lineid;
        this.LineType=linetype;
        this.Description=description;
        this.Color=color;
        this.Stations=new Array<Station>();
    }
}


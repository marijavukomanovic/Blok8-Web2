import { GeoLocation } from "./geolocation";

export class MarkerInfo {
    iconUrl: string;
    title: string;
    location: GeoLocation;

    constructor(location: GeoLocation, icon: string, title:string){
        this.iconUrl = icon;
        this.title = title;
        this.location = location;
    }
}
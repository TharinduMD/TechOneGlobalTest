import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

const apiURL = "https://localhost:7297";

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http : HttpClient) { 
    
  }
  GetCityList(){
    return this.http.get<any>(`${apiURL}/cities`);
  }
  GetWeatherDetails(cityId : string){
    return this.http.get<any>(`${apiURL}/cities/weather-details/`+cityId);
  }
  AddCity(cityData : any){
    return this.http.post<any>(`${apiURL}/cities`, cityData);
  }
}

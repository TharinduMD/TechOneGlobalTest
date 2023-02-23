import { Component, OnInit, TemplateRef } from '@angular/core';
import {FormControl, Validators, FormGroup, FormBuilder} from '@angular/forms';
import { WeatherService } from 'src/app/services/weather.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-citydetails',
  templateUrl: './citydetails.component.html',
  styleUrls: ['./citydetails.component.css']
})
export class CitydetailsComponent implements OnInit {
  addCity : FormGroup
  passCityId : FormGroup
  cityArr = [];
  weatherArr : any = [];
  constructor(private formBuilder : FormBuilder, 
    private weatherservice : WeatherService, 
    private matDialog : MatDialog ){

  }
  ngOnInit(): void {
    this.weatherservice.GetCityList().subscribe({
      next : (cityDetails : any) => {
        this.cityArr = cityDetails.result;
      },
      error : (error) => {
        console.log(error);
      }
    })
    this.passCityId = this.formBuilder.group({
        cityId : [null],
      
    })
    this.addCity = this.formBuilder.group({
        cityName : [''],
        latitude : [null],
        longitude : [null]      
    })
  }
  changeCity(){
    this.weatherArr = [];
    this.weatherservice.GetWeatherDetails(this.passCityId.value.cityId).subscribe({
      next : (wetherDetails : any) => {
        this.weatherArr = wetherDetails.result;
      },
      error : (error) => {
        console.log(error);
      }
    })
  }
  addCityPopUp(addCitypopup : TemplateRef<any>){
    this.matDialog.open(addCitypopup);
  }
  SaveCity(){
    this.weatherservice.AddCity(this.addCity.value).subscribe({
      next : (saveCityResponse : any) => {
        alert("Succeefully added");
        this.updateCityList();
      },
      error : (error) => {
        console.log(error);
      }
    })
  }
  updateCityList(){
    this.weatherservice.GetCityList().subscribe({
      next : (cityDetails : any) => {
        this.cityArr = cityDetails.result;
      },
      error : (error) => {
        console.log(error);
      }
    })
  }
}

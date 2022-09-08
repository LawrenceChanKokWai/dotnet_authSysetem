import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Geco-App';
  name = 'Lawrence'
  isLogin:boolean = false;

  public ProductList : any = [
    {productId: 1000, productName: "Apple", price: 2.00},
    {productId: 1001, productName: "Watermelon", price: 9.00},
    {productId: 1002, productName: "Durian", price: 7.00},
  ]
  
  constructor(){

    /*Logic*/
    this.isLogin = true;
  } 
  




}

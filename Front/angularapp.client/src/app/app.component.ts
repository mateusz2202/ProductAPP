import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface Product {
  Code: string;
  Name: number;
  Price: number;  
}

interface DataResponse {
  Data: Product[];
  Succeeded: boolean;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})


export class AppComponent implements OnInit {
  public products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getProducts();
  }
  

  getProducts() {
    const url = 'https://localhost:7200/Product';
    this.http.get<DataResponse>(url).subscribe(
      (result) => {
        this.products = result.Data;
        console.log(result.Data);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'angularapp.client';
}

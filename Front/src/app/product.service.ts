import { HttpClient } from '@angular/common/http'; 
import { Injectable } from '@angular/core'; 
import { Observable } from 'rxjs';

interface Product {
  Code: string;
  Name: number;
  Price: number;  
}

interface DataResponse {
  Data: Product[];
  Succeeded: boolean;
}

@Injectable({
    providedIn: 'root',
})
export class ProductService {
    baseUrl: string = "https://localhost:7200/";

    constructor(private httpClient: HttpClient) { }

    addEmployee(data: any): Observable<any> {
        return this.httpClient.post(this.baseUrl + 'Product', data);
    }   

    getEmployeeList(): Observable<DataResponse> {
      var result=this.httpClient.get<DataResponse>(this.baseUrl + 'Product');
        return result;
    }

    
}

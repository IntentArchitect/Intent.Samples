import { CreateCustomerCommand } from './../models/backend/services/customers/create-customer-command';
import { CustomerDto } from './../models/backend/services/customers/customer-dto';
import { CustomerSummaryDto } from './../models/backend/services/customers/customer-summary-dto';
import { GetCustomersQuery } from './../models/backend/services/customers/get-customers-query';
import { UpdateCustomerCommand } from './../models/backend/services/customers/update-customer-command';
import { JsonResponse } from './../models/json-response';
import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { timeout, retry, map } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedResult } from './../models/paged-result';


@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  private readonly baseUrl = environment.intentSampleAngularBackendServiceConfig.services?.customersService?.baseUrl ?? environment.intentSampleAngularBackendServiceConfig.baseUrl;
  private readonly timeoutMs = environment.intentSampleAngularBackendServiceConfig.services?.customersService?.timeoutMs ?? environment.intentSampleAngularBackendServiceConfig.timeoutMs ?? 10_000;
  private readonly retries = environment.intentSampleAngularBackendServiceConfig.services?.customersService?.retries ?? environment.intentSampleAngularBackendServiceConfig.retries ?? 0;

  constructor(private httpClient: HttpClient) {
  }

  public createCustomer(command: CreateCustomerCommand): Observable<string> {
    const url = `${this.baseUrl}api/customers`;
    return this.httpClient.post<JsonResponse<string>>(url, command)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: JsonResponse<string>) => {
          return response.value;
      }));
  }

  public getCustomerById(id: string): Observable<CustomerDto> {
    const url = `${this.baseUrl}api/customers/${encodeURIComponent(id)}`;
    return this.httpClient.get<CustomerDto>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: CustomerDto) => {
          return response;
      }));
  }

  public getCustomers(query: GetCustomersQuery): Observable<PagedResult<CustomerSummaryDto>> {
    const url = `${this.baseUrl}api/customers`;
    let httpParams = new HttpParams()
      .set("pageNo", query.pageNo)
      .set("pageSize", query.pageSize);
    
    if(query.searchTerm != null) {
      httpParams = httpParams.set("searchTerm", query.searchTerm);
    }
    
    if(query.orderBy != null) {
      httpParams = httpParams.set("orderBy", query.orderBy);
    }
    
    if(query.isActive != null) {
      httpParams = httpParams.set("isActive", query.isActive);
    }
    return this.httpClient.get<PagedResult<CustomerSummaryDto>>(url, { params: httpParams })
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: PagedResult<CustomerSummaryDto>) => {
          return response;
      }));
  }

  public updateCustomer(command: UpdateCustomerCommand): Observable<void> {
    const url = `${this.baseUrl}api/customers/${encodeURIComponent(command.id)}`;
    return this.httpClient.put<void>(url, command)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }
}
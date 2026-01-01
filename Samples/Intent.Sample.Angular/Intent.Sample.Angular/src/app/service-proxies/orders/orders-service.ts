import { CreateOrderCommand } from './../models/backend/services/orders/create-order-command';
import { OrderDto } from './../models/backend/services/orders/order-dto';
import { OrderSummaryDto } from './../models/backend/services/orders/order-summary-dto';
import { JsonResponse } from './../models/json-response';
import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { timeout, retry, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  private readonly baseUrl = environment.intentSampleAngularBackendServiceConfig.services?.ordersService?.baseUrl ?? environment.intentSampleAngularBackendServiceConfig.baseUrl;
  private readonly timeoutMs = environment.intentSampleAngularBackendServiceConfig.services?.ordersService?.timeoutMs ?? environment.intentSampleAngularBackendServiceConfig.timeoutMs ?? 10_000;
  private readonly retries = environment.intentSampleAngularBackendServiceConfig.services?.ordersService?.retries ?? environment.intentSampleAngularBackendServiceConfig.retries ?? 0;

  constructor(private httpClient: HttpClient) {
  }

  public createOrder(command: CreateOrderCommand): Observable<string> {
    const url = `${this.baseUrl}api/orders`;
    return this.httpClient.post<JsonResponse<string>>(url, command)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: JsonResponse<string>) => {
          return response.value;
      }));
  }

  public deleteOrder(id: string): Observable<void> {
    const url = `${this.baseUrl}api/orders/${encodeURIComponent(id)}`;
    return this.httpClient.delete<void>(url)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }

  public getOrderById(id: string): Observable<OrderDto> {
    const url = `${this.baseUrl}api/orders/${encodeURIComponent(id)}`;
    return this.httpClient.get<OrderDto>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: OrderDto) => {
          return response;
      }));
  }

  public getOrders(): Observable<OrderSummaryDto[]> {
    const url = `${this.baseUrl}api/orders`;
    return this.httpClient.get<OrderSummaryDto[]>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: OrderSummaryDto[]) => {
          return response;
      }));
  }
}
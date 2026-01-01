import { CreateDiscountCommand } from './../models/backend/services/discounts/create-discount-command';
import { DiscountDto } from './../models/backend/services/discounts/discount-dto';
import { UpdateDiscountCommand } from './../models/backend/services/discounts/update-discount-command';
import { JsonResponse } from './../models/json-response';
import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { timeout, retry, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DiscountsService {
  private readonly baseUrl = environment.intentSampleAngularBackendServiceConfig.services?.discountsService?.baseUrl ?? environment.intentSampleAngularBackendServiceConfig.baseUrl;
  private readonly timeoutMs = environment.intentSampleAngularBackendServiceConfig.services?.discountsService?.timeoutMs ?? environment.intentSampleAngularBackendServiceConfig.timeoutMs ?? 10_000;
  private readonly retries = environment.intentSampleAngularBackendServiceConfig.services?.discountsService?.retries ?? environment.intentSampleAngularBackendServiceConfig.retries ?? 0;

  constructor(private httpClient: HttpClient) {
  }

  public createDiscount(command: CreateDiscountCommand): Observable<string> {
    const url = `${this.baseUrl}api/discounts`;
    return this.httpClient.post<JsonResponse<string>>(url, command)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: JsonResponse<string>) => {
          return response.value;
      }));
  }

  public deleteDiscount(id: string): Observable<void> {
    const url = `${this.baseUrl}api/discounts/${encodeURIComponent(id)}`;
    return this.httpClient.delete<void>(url)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }

  public getDiscountById(id: string): Observable<DiscountDto> {
    const url = `${this.baseUrl}api/discounts/${encodeURIComponent(id)}`;
    return this.httpClient.get<DiscountDto>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: DiscountDto) => {
          return response;
      }));
  }

  public getDiscounts(): Observable<DiscountDto[]> {
    const url = `${this.baseUrl}api/discounts`;
    return this.httpClient.get<DiscountDto[]>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: DiscountDto[]) => {
          return response;
      }));
  }

  public updateDiscount(command: UpdateDiscountCommand): Observable<void> {
    const url = `${this.baseUrl}api/discounts/${encodeURIComponent(command.id)}`;
    return this.httpClient.put<void>(url, command)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }
}
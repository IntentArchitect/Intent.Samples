import { BrandDto } from './../models/backend/services/brands/brand-dto';
import { UpdateBrandCommand } from './../models/backend/services/brands/update-brand-command';
import { JsonResponse } from './../models/json-response';
import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { timeout, retry, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class BrandsService {
  private readonly baseUrl = environment.intentSampleAngularBackendServiceConfig.services?.brandsService?.baseUrl ?? environment.intentSampleAngularBackendServiceConfig.baseUrl;
  private readonly timeoutMs = environment.intentSampleAngularBackendServiceConfig.services?.brandsService?.timeoutMs ?? environment.intentSampleAngularBackendServiceConfig.timeoutMs ?? 10_000;
  private readonly retries = environment.intentSampleAngularBackendServiceConfig.services?.brandsService?.retries ?? environment.intentSampleAngularBackendServiceConfig.retries ?? 0;

  constructor(private httpClient: HttpClient) {
  }

  public createBrand(name: string): Observable<string> {
    const url = `${this.baseUrl}api/brands`;
    return this.httpClient.post<JsonResponse<string>>(url, { name })
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: JsonResponse<string>) => {
          return response.value;
      }));
  }

  public getBrandById(id: string): Observable<BrandDto> {
    const url = `${this.baseUrl}api/brands/${encodeURIComponent(id)}`;
    return this.httpClient.get<BrandDto>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: BrandDto) => {
          return response;
      }));
  }

  public getBrands(): Observable<BrandDto[]> {
    const url = `${this.baseUrl}api/brands`;
    return this.httpClient.get<BrandDto[]>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: BrandDto[]) => {
          return response;
      }));
  }

  public updateBrand(command: UpdateBrandCommand): Observable<void> {
    const url = `${this.baseUrl}api/brands/${encodeURIComponent(command.id)}`;
    return this.httpClient.put<void>(url, command)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }
}
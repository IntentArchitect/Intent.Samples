import { TitleDto } from './../models/backend/services/titles/title-dto';
import { UpdateTitleCommand } from './../models/backend/services/titles/update-title-command';
import { JsonResponse } from './../models/json-response';
import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { timeout, retry, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class TitlesService {
  private readonly baseUrl = environment.intentSampleAngularBackendServiceConfig.services?.titlesService?.baseUrl ?? environment.intentSampleAngularBackendServiceConfig.baseUrl;
  private readonly timeoutMs = environment.intentSampleAngularBackendServiceConfig.services?.titlesService?.timeoutMs ?? environment.intentSampleAngularBackendServiceConfig.timeoutMs ?? 10_000;
  private readonly retries = environment.intentSampleAngularBackendServiceConfig.services?.titlesService?.retries ?? environment.intentSampleAngularBackendServiceConfig.retries ?? 0;

  constructor(private httpClient: HttpClient) {
  }

  public createTitle(name: string): Observable<string> {
    const url = `${this.baseUrl}api/titles`;
    return this.httpClient.post<JsonResponse<string>>(url, { name })
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: JsonResponse<string>) => {
          return response.value;
      }));
  }

  public getTitleById(id: string): Observable<TitleDto> {
    const url = `${this.baseUrl}api/titles/${encodeURIComponent(id)}`;
    return this.httpClient.get<TitleDto>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: TitleDto) => {
          return response;
      }));
  }

  public getTitles(): Observable<TitleDto[]> {
    const url = `${this.baseUrl}api/titles`;
    return this.httpClient.get<TitleDto[]>(url)
      .pipe(
        timeout(this.timeoutMs),
        retry(this.retries),
        map((response: TitleDto[]) => {
          return response;
      }));
  }

  public updateTitle(command: UpdateTitleCommand): Observable<void> {
    const url = `${this.baseUrl}api/titles/${encodeURIComponent(command.id)}`;
    return this.httpClient.put<void>(url, command)
      .pipe(
        timeout(this.timeoutMs), 
        retry(this.retries)
      );
  }
}
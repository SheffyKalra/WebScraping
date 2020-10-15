import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { GlobalConstants } from '../Common/global.constants';
import { from, Observable, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebScrapingService {
  public options;
  Url: string;
  token: string;
  header: any;
  constructor(private http: HttpClient,) {
    this.Url = GlobalConstants.apiURL + "WebScraping/GetData";
    this.setHeaders();
  }

  setHeaders() {
    this.options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Cache-Control': 'no-cache',
        'Pragma': 'no-cache',
        'Expires': 'Sat, 01 Jan 2000 00:00:00 GMT'
      }),
      withCredentials: false
    };
  }

  GetScrapedData(siteName1): Observable<any>{
    const siteName = JSON.stringify(siteName1);
    return this.http.post<any>("https://localhost:44362/webscraping/GetScrapedData", siteName, this.options)
  }


  DownloadPdf(htmlData): Observable<any> {
    const htmldataString = JSON.stringify(htmlData);
    return this.http.post<any>("https://localhost:44362/webscraping/DownloadPdf", htmldataString, this.options)
  }

  
}

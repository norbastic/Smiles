import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Constants } from "../constants";

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  private subUrl = 'upload';

  constructor(private constants: Constants, private http: HttpClient) { }

  public uploadFile(formData: FormData): Observable<number> {
    return this.http.post<number>([this.constants.API_ENDPOINT, this.subUrl].join('/'), formData);
  }
}

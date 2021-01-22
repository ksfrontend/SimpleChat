import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CommonService } from './common.service';

const apiURL = environment.apiUrl;
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private commonSerive: CommonService) { }

  authorize(model: any, callBack: Function) {
    this.commonSerive.ApiCall('/api/login/Authenticate', model, false,
      function (response: any) {
        if (typeof (callBack) === 'function') {
          callBack(response);
        }
      })
  }
}

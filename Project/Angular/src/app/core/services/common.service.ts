import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { LoggedInUserModel, ServiceResponse } from '../models/common';
import { StorageService } from './localstorage.service';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpClient,
    private storageService: StorageService,
    private toastr: ToastrService) {

  }

  /* #region Set/Get Current loggedin User */
  public GetCurrentUser(): LoggedInUserModel {
    // let model = JSON.parse(localStorage.getItem('currentUser'))
    // if (model)
    //     return model as LoggedInUserModel;
    // return null;
    let model = JSON.parse(this.storageService.getStorage('currentUser'));
    if (model)
      return model as LoggedInUserModel;
    return null;

  }
  public SetCurrentUser(currentUser: LoggedInUserModel) {
    //localStorage.setItem('currentUser', JSON.stringify(currentUser));
    this.storageService.setStorage('currentUser', currentUser, null);
  }
  public RemoveCurrentUser() {
    // localStorage.removeItem('currentUser');
    this.storageService.removeStorage('currentUser');
  }
  /* #endregion Set/Get Current loggedin User */

  public ApiCall(url: string, postData: any, isShowLoader: boolean, successCall: any): void {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    let requestData = JSON.stringify(postData);//(new ServiceRequest("KEY", "Token", postData));
    this.http.post<any>(environment.apiUrl + url, requestData, httpOptions)
      .subscribe((response) => {
        if (typeof (successCall) == 'function') {
          successCall(response);
        }
      },
        (error) => {
          //OnError
        },
        () => {
          //After Complete
        })
  }

  public ShowMessage(model: ServiceResponse) {
    if (model.IsSuccess) {
      this.toastr.success(model.Message, '', {
        closeButton: true
      });
    } else {
      this.toastr.error(model.Message, '', {
        closeButton: true
      });
    }
  }

  public ShowMessageByType(type: string, message: string) {
    if (type === 'success') {
        this.toastr.success(message, '', {
            closeButton: true
        });
    } else if (type === 'error') {
        this.toastr.error(message, '', {
            closeButton: true
        });
    } else if (type === 'warning') {
        this.toastr.warning(message, '', {
            closeButton: true
        });
    } else {
        this.toastr.info(message, '', {
            closeButton: true
        });
    }
}
}

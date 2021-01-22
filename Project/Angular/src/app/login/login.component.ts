import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoggedInUserModel } from '../core/models/common';
import { AuthService } from '../core/services/auth.service';
import { ChatService } from '../core/services/chat.service';
import { CommonService } from '../core/services/common.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public email: any = 'test';
  public password: any = 'test';

  constructor(
    public router: Router,
    private authService: AuthService,
    private toastr: ToastrService,
    private commonService: CommonService,
    private chatservice: ChatService
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.authorize({ UserName: this.email, Password: this.password }, (response: any) => {
      if (response) {
        if (response.IsSuccess) {
          this.commonService.SetCurrentUser(response.Data as LoggedInUserModel);
          this.chatservice.startConnection();
          this.router.navigateByUrl('dashboard');
        } else {
          this.toastr.error(response.Message, 'Error');
        }
      }
    });
  }
}

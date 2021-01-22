import {Component, OnInit, Output, EventEmitter, AfterContentInit} from '@angular/core';
import { LoggedInUserModel, ServiceResponse } from '../core/models/common';
import { CommonService } from '../core/services/common.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit, AfterContentInit {
  @Output() userData: EventEmitter<any> = new EventEmitter();
  currentUser: LoggedInUserModel;
  public selectedUser: any;
  public userList: any = [];

  constructor(private commonService: CommonService) {
    this.currentUser = this.commonService.GetCurrentUser();
  }

  ngOnInit(): void {
    // this.userList = this.userListAll;
  }

  ngAfterContentInit(): void {
    setTimeout(() => {
      this.getUserList();
    });
  }

  public openChat(obj): void {
    this.selectedUser = obj.UserId;
    this.userData.emit(obj);
  }

  public getUserList(): void {
    this.commonService.ApiCall('/api/user/GetUserList', null, true,
      (response: ServiceResponse) => {

        if (response.IsSuccess) {
          const users = response.Data as any;

          if (users && users.length > 0) {
            this.userList = users.filter(m => m.UserId !== this.currentUser.UserId);
          }
        }
        else
          this.commonService.ShowMessage(response);
      });
  }
}

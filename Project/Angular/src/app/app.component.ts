import { Component } from '@angular/core';
import { LoggedInUserModel } from './core/models/common';
import { ChatService } from './core/services/chat.service';
import { CommonService } from './core/services/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SimpleChat';
  currentUser: LoggedInUserModel;
  constructor(private commonService: CommonService,
              private chatService: ChatService) {
    this.currentUser = this.commonService.GetCurrentUser();
    (<any>window).app = this;
  }


  ngAfterContentChecked(): void {
  }


  ngOnInit() {
    if (this.currentUser) {
      this.chatService.startConnection();
    }
  }
}

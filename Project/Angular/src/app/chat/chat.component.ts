import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { LoggedInUserModel, SendChatMessageModel, ServiceResponse } from '../core/models/common';
import { ChatService } from '../core/services/chat.service';
import { CommonService } from '../core/services/common.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  currentUser: LoggedInUserModel;
  public userData: any;
  public isActiveChat: boolean = false;
  public activeChatList = [];
  MessageModel: SendChatMessageModel = new SendChatMessageModel();


  constructor(private chatService: ChatService,
              private commonService: CommonService) {
    this.currentUser = this.commonService.GetCurrentUser();

    chatService.getNewMessage().subscribe((newMessage: any) => {

      if (newMessage.ReceiverId === this.currentUser.UserId) {
        this.commonService.SetCurrentUser(this.currentUser);


        if (this.userData && this.userData.UserId === newMessage.SenderId) {
          this.getChatList(this.currentUser.UserId, this.userData.UserId);
        }
      }


    });
  }

  ngOnInit(): void {
  }

  sendMessage() {
    if (this.MessageModel.TextMessage !== null && this.MessageModel.TextMessage !== '') {
      this.MessageModel.SenderId = this.currentUser.UserId;
      this.MessageModel.ReceiverId = this.userData.UserId;
      this.chatService.onSendClick(this.MessageModel, () => {
        if (!this.activeChatList)
          this.activeChatList = [];

        let chatObj = Object.assign({}, this.MessageModel) as any;
        chatObj.CreatedDate = new Date();
        this.activeChatList.push(chatObj);
        this.MessageModel.TextMessage = '';
      });
    }
  }

  userInfo(obj) {
    this.isActiveChat = true;
    this.userData = obj;
    this.getChatList(this.currentUser.UserId, obj.UserId);
  }

  getChatList(senderId: number, receiverId: number) {

    this.activeChatList = [];

    this.chatService.GetMessages(senderId, receiverId, (response: ServiceResponse) => {
      if (response.IsSuccess) {

        this.activeChatList = response.Data as any;
      }
    });

  }
}

import { Injectable } from '@angular/core';
import { SignalR, SignalRConnection, ISignalRConnection } from 'ng2-signalr';
import { Observable, BehaviorSubject } from 'rxjs';
import { share } from 'rxjs/operators';
import { ChatMessage, LoggedInUserModel, SendChatMessageModel, ServiceResponse } from '../models/common';
import { CommonService } from './common.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  connection: ISignalRConnection;
  currentUser: LoggedInUserModel;

  sendMessageModel: SendChatMessageModel = new SendChatMessageModel();
  public IsConnected: boolean = false;

  constructor(private hubConnection: SignalR, private commonService: CommonService) {
    (<any>window).chatComponent = this;
    this.currentUser = this.commonService.GetCurrentUser();
  }

  newmessage$ = new BehaviorSubject<ChatMessage>(new ChatMessage());
  public changes = this.newmessage$.asObservable().pipe(share());

  getNewMessage(): Observable<ChatMessage> {
    return this.newmessage$.asObservable();
  }

  public receiveMessage(messagesmodel: ChatMessage) {
    let self = this;
    if (messagesmodel !== null) {
      this.newmessage$.next(messagesmodel);
    }
  }

  public onSendClick(massagemode: SendChatMessageModel, callBack: Function) {
    let self = this;
    this.connection.invoke('privatemessage', massagemode);
    callBack();
  }


  public GetMessages(senderId: number, receiverId: number, callBack: Function) {
    let self = this;
    self.commonService.ApiCall('/api/user/GetChatList', { SenderId: senderId, ReceiverId: receiverId }, true,
      function (response: ServiceResponse) {
        if (typeof (callBack) == 'function') {
          callBack(response);
        }
      });
  }

  public startConnection = () => {
    if (!this.currentUser) {
      this.currentUser = this.commonService.GetCurrentUser();
    }
    this.hubConnection.connect({ jsonp: true, qs: { 'UserId': this.currentUser.UserId }, hubName: 'ChatHub', url: environment.apiUrl }).then((conn) => {
      this.connection = conn;
      this.sendMessageModel.SenderId = this.currentUser.UserId;
      let listner = conn.listenFor('Ping');
      listner.subscribe(() => {
        //console.info('Ping from server!');
      });
      conn.listenForRaw('receiveMessage').subscribe((data: any[]) => this.receiveMessage(data[0]));

      this.IsConnected = true;
    }).catch((e) => {
      //console.log('Error connecting to hub:  ' + e);
    });
  }

}

import { Component, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { ChatComponent } from '../chat/chat.component';
import { ContactComponent } from '../contact/contact.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild(ContactComponent, { static: false }) ContactComponent;
  @ViewChild(ChatComponent, { static: false }) ChatComponent;
  @Output() userInfo: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  getUserData(obj): void {
    this.userInfo.emit(obj)
    this.ChatComponent.userInfo(obj)
  }

}

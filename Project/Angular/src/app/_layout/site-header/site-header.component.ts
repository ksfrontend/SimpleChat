import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { ChatService } from "src/app/core/services/chat.service";
import { CommonService } from "src/app/core/services/common.service";

@Component({
  selector: "app-site-header",
  templateUrl: "./site-header.component.html",
  styleUrls: ["./site-header.component.scss"]
})
export class SiteHeaderComponent implements OnInit {
  constructor(private commonService: CommonService, private route: Router,
    private chatservice: ChatService) { }

  ngOnInit() {

  }

  signout() {
    this.commonService.RemoveCurrentUser();
    this.chatservice.connection.stop();
    this.route.navigateByUrl('/login');
  }
}

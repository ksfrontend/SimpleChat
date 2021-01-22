// import { ComponentFixture, TestBed } from '@angular/core/testing';
//
// import { ChatComponent } from './chat.component';
// import {CommonService} from '../core/services/common.service';
// import {HttpClient} from '@aspnet/signalr';
// import {SignalR, SignalRConfiguration} from 'ng2-signalr';
// import {SignalRMock} from '../app.component.spec';
// import {HttpClientModule} from '@angular/common/http';
// import {ToastrModule} from 'ngx-toastr';
// import {RouterTestingModule} from '@angular/router/testing';
// import {CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
//
//
// describe('ChatComponent', () => {
//   let component: ChatComponent;
//   let fixture: ComponentFixture<ChatComponent>;
//
//   beforeEach(async () => {
//     await TestBed.configureTestingModule({
//       imports: [
//         ToastrModule.forRoot(),
//         RouterTestingModule.withRoutes([]),
//         HttpClientModule
//       ],
//       declarations: [ ChatComponent ],
//       providers: [
//         CommonService,
//         HttpClient,
//         SignalR,
//         {provide: SignalR, useClass: SignalRMock},
//         SignalRConfiguration
//       ],
//       schemas: [CUSTOM_ELEMENTS_SCHEMA]
//     })
//     .compileComponents();
//   });
//
//   beforeEach(() => {
//     fixture = TestBed.createComponent(ChatComponent);
//     component = fixture.componentInstance;
//     component.currentUser = {
//       UserId: 2,
//       UserName: '',
//       Token: ''
//     };
//     fixture.detectChanges();
//   });
//
//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });
// });

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import {ToastrModule} from 'ngx-toastr';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientModule} from '@angular/common/http';
import {CommonService} from '../core/services/common.service';
import {HttpClient} from '@aspnet/signalr';
import {SignalR, SignalRConfiguration} from 'ng2-signalr';
import {SignalRMock} from '../app.component.spec';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot(),
        RouterTestingModule.withRoutes([]),
        HttpClientModule
      ],
      declarations: [ LoginComponent ],
      providers: [
        CommonService,
        HttpClient,
        SignalR,
        {provide: SignalR, useClass: SignalRMock},
        SignalRConfiguration
      ],
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

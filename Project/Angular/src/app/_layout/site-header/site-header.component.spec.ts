import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteHeaderComponent } from './site-header.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {CommonService} from '../../core/services/common.service';
import {HttpClient} from '@aspnet/signalr';
import {HttpClientModule} from '@angular/common/http';
import {ToastrModule} from 'ngx-toastr';
import {RouterTestingModule} from '@angular/router/testing';
import {SignalR, SignalRConfiguration} from 'ng2-signalr';
import {SignalRMock} from '../../app.component.spec';

describe('SiteHeaderComponent', () => {
  let component: SiteHeaderComponent;
  let fixture: ComponentFixture<SiteHeaderComponent>;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot(),
        HttpClientModule,
        RouterTestingModule.withRoutes([]),
      ],
      declarations: [ SiteHeaderComponent ],
      providers: [
        CommonService,
        HttpClient,
        SignalR,
        {provide: SignalR, useClass: SignalRMock},
        SignalRConfiguration
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { CommonService } from './core/services/common.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import {HttpClientModule} from '@angular/common/http';
import {HttpClient} from '@aspnet/signalr';
import {SignalR, SignalRConfiguration} from 'ng2-signalr';

export class SignalRMock {
  public createConnection(options?: any): any {
    return null;
  }

  public connect(options?: any): Promise<any> {
    return new Promise<any>(null);
  }
}

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ToastrModule.forRoot(),
        RouterTestingModule.withRoutes([]),
        HttpClientModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        CommonService,
        HttpClient,
        SignalR,
        {provide: SignalR, useClass: SignalRMock},
        SignalRConfiguration
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'SimpleChat'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('SimpleChat');
  });
});

import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { share } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private onSubject = new Subject<{ key: string, value: any }>();
  public changes = this.onSubject.asObservable().pipe(share());

  constructor() {
      this.start();
  }

  ngOnDestroy() {
      this.stop();
  }

  public removeStorage(name) {
      try {
          localStorage.removeItem(name);
          localStorage.removeItem(name + '_expiresIn');
          this.onSubject.next({ key: name, value: null });
      } catch (e) {
          //console.log('removeStorage: Error removing key [' + name + '] from localStorage: ' + JSON.stringify(e));
          return false;
      }
      return true;
  }

  /*  getStorage: retrieves a key from localStorage previously set with setStorage().
      params:
          key <string> : localStorage key
      returns:
          <string> : value of localStorage key
          null : in case of expired key or failure
   */
  public getStorage(key) {

      var now = Date.now();  //epoch time, lets deal only with integer
      // set expiration for storage
      var expiresIn = +localStorage.getItem(key + '_expiresIn');
      if (expiresIn === undefined || expiresIn === null) { expiresIn = 0; }

      if (expiresIn < now) {// Expired
          this.removeStorage(key);
          return null;
      } else {
          try {
              var value = localStorage.getItem(key);
              return value;
          } catch (e) {
              //console.log('getStorage: Error reading key [' + key + '] from localStorage: ' + JSON.stringify(e));
              return null;
          }
      }
  }

  /*  setStorage: writes a key into localStorage setting a expire time
      params:
          key <string>     : localStorage key
          value <string>   : localStorage value
          expires <number> : number of seconds from now to expire the key
      returns:
          <boolean> : telling if operation succeeded
   */
  public setStorage(key, value, expires) {

      if (expires === undefined || expires === null) {
          expires = (24 * 60 * 60);  // default: seconds for 1 day
      } else {
          expires = Math.abs(expires); //make sure it's positive
      }

      var now = Date.now();  //millisecs since epoch time, lets deal only with integer
      var schedule: string = (now + expires * 1000).toString();
      try {
          localStorage.setItem(key, JSON.stringify(value));
          localStorage.setItem(key + '_expiresIn', schedule);
          this.onSubject.next({ key: key, value: value })
      } catch (e) {
          // console.log('setStorage: Error setting key [' + key + '] in localStorage: ' + JSON.stringify(e));
          return false;
      }
      return true;
  }

  private start(): void {
      window.addEventListener('storage', this.storageEventListener.bind(this));
  }

  private storageEventListener(event: StorageEvent) {
      if (event.storageArea === localStorage) {
          let v;
          try { v = JSON.parse(event.newValue); }
          catch (e) { v = event.newValue; }
          this.onSubject.next({ key: event.key, value: v });
      }
  }

  private stop(): void {
      window.removeEventListener('storage', this.storageEventListener.bind(this));
      this.onSubject.complete();
  }

}

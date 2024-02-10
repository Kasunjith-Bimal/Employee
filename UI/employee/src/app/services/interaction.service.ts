import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {
  public loggedInUserSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor() { }
}

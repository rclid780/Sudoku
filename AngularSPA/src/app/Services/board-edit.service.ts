import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BoardEditService {

  constructor() { }
  cellData: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  currentData = this.cellData.asObservable();

  updateBoardData(data: number){
    this.cellData.next(data);
  }
}
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BoardEditService } from '../Services/board-edit.service';

@Component({
  selector: 'app-board-update',
  imports: [FormsModule],
  templateUrl: './board-update.component.html',
  styleUrl: './board-update.component.css'
})
export class BoardUpdateComponent {
  constructor(private boardEditService: BoardEditService){}

  submitHandler(data: number){
    this.boardEditService.updateBoardData(data);
  }
}

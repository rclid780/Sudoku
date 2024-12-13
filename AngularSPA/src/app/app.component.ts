import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { BoardUpdateComponent } from './board-update/board-update.component';
import { BoardControlsComponent } from "./board-controls/board-controls.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, BoardComponent, BoardUpdateComponent, BoardControlsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Sudoku Angular';

  @ViewChild(BoardComponent) board!: BoardComponent;

  constructor(){}

  recieveCheckEvent() {
    this.board.Check()
  }

  recieveNewEvent() {
    this.board.NewGame()
  }

  recieveHighlightEvent(checked: boolean){
    this.board.Highlight(checked)
  }
}

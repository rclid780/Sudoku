import { Component, numberAttribute } from '@angular/core';
import { NgFor } from '@angular/common';
import { BoardEditService } from '../Services/board-edit.service'; 
import { deepCopy } from '@angular-devkit/core/src/utils/object';

@Component({
  selector: 'app-board',
  imports: [NgFor],
  templateUrl: './board.component.html',
  styleUrl: './board.component.css'
})
export class BoardComponent {
  constructor(private boardEdit: BoardEditService){}

  boardData: number[][] = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
  ];
  solution: number[][] = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
  ];
  puzzle: number[][] = [
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 0, 0],
  ];

  row: number = 0;
  col: number = 0;
  highlight: boolean = false;
  inGame: boolean = false;

  ngOnInit(): void {
    this.boardEdit.currentData.subscribe(
      boardData => {
        if(this.puzzle[this.row][this.col] == 0)
        {
          this.boardData[this.row][this.col] = boardData
        }
      }
    );
  }

  clickMethod(r: number, c: number) {
    this.row = r;
    this.col = c;
  }

  getRowSection(r: number): number[] {
    return this.boardData[r]
  }

  Check(){
    for(var i: number = 0; i < 9; i++){
      for(var j: number = 0; j < 9; j++){
        if(this.boardData[i][j] != this.solution[i][j])
        {
          alert('board does not match solution at (' + i + ',' + j + ')');
          return;
        }
      }
    }
    alert('Congratulations, you solved this sudoku puzzle!')
  }

  Highlight(isHighlighted: boolean){
    this.highlight = isHighlighted
  }

  NewGame(){
    if(this.inGame && !confirm("All progress on this board will be lost. Start a new game?"))
    {
      return;
    }

    this.inGame = true;
    //TODO: get this data from the C# sudoku library by httpClient to .net hosted rest service
    //need to enable CORS(localhost:4200) to prevent issues connecting local to local 
    this.boardData = [
      [0, 0, 5, 0, 2, 0, 0, 1, 9],
      [2, 0, 4, 0, 0, 3, 0, 0, 8],
      [0, 0, 0, 0, 0, 0, 0, 4, 7],
      [0, 0, 2, 5, 0, 0, 0, 3, 4],
      [0, 8, 0, 0, 0, 6, 1, 7, 2],
      [1, 0, 0, 0, 0, 0, 8, 0, 5],
      [0, 0, 8, 4, 0, 9, 0, 0, 0],
      [0, 0, 7, 0, 1, 0, 5, 0, 0],
      [0, 0, 1, 0, 7, 0, 4, 0, 0]
    ];

    //deep clone the game board to save the base state
    this.puzzle = deepCopy(this.boardData);

    this.solution = [
      [8, 7, 5, 6, 2, 4, 3, 1, 9],
      [2, 1, 4, 7, 9, 3, 6, 5, 8],
      [9, 3, 6, 1, 5, 8, 2, 4, 7],
      [7, 6, 2, 5, 8, 1, 9, 3, 4],
      [5, 8, 3, 9, 4, 6, 1, 7, 2],
      [1, 4, 9, 2, 3, 7, 8, 6, 5],
      [3, 5, 8, 4, 6, 9, 7, 2, 1],
      [4, 9, 7, 3, 1, 2, 5, 8, 6],
      [6, 2, 1, 8, 7, 5, 4, 9, 3]
    ];
  }
}

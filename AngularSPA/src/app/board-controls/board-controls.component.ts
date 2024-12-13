import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-board-controls',
  imports: [],
  templateUrl: './board-controls.component.html',
  styleUrl: './board-controls.component.css'
})
export class BoardControlsComponent {

  @Output() checkEvent = new EventEmitter();
  @Output() newEvent = new EventEmitter();
  @Output() highlightEvent: EventEmitter<boolean> = new EventEmitter();

  constructor(){}

  sendCheckEvent(){
    this.checkEvent.emit()
  }

  sendNewEvent(){
    this.newEvent.emit()
  }

  sendHighlightEvent(event: Event){
    const target = event.target as HTMLInputElement;
    this.highlightEvent.emit(target.checked)
  }
}

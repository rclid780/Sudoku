import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardControlsComponent } from './board-controls.component';

describe('BoardControlsComponent', () => {
  let component: BoardControlsComponent;
  let fixture: ComponentFixture<BoardControlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoardControlsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoardControlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

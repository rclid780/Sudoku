import { TestBed } from '@angular/core/testing';

import { BoardEditService } from './board-edit.service';

describe('BoardEditService', () => {
  let service: BoardEditService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BoardEditService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

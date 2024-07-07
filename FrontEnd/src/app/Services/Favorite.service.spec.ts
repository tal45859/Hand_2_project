/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FavoriteService } from './Favorite.service';

describe('Service: Favorite', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FavoriteService]
    });
  });

  it('should ...', inject([FavoriteService], (service: FavoriteService) => {
    expect(service).toBeTruthy();
  }));
});

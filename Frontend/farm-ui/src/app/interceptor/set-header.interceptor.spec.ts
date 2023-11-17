import { TestBed } from '@angular/core/testing';

import { SetHeaderInterceptor } from './set-header.interceptor';

describe('SetHeaderInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      SetHeaderInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: SetHeaderInterceptor = TestBed.inject(SetHeaderInterceptor);
    expect(interceptor).toBeTruthy();
  });
});

import { gameService } from '../app/components/services/Game.service';
import { HttpClient } from '@angular/common/http';
import { of, throwError } from 'rxjs';

describe('ApiService', () => {
  let apiService: gameService
  let httpMock: any;

  beforeEach(() => {
    httpMock = {
      post: jest.fn()
    };

    apiService = new gameService(httpMock);
  });

  it('should send a POST request to get game planets', () => {
    const expectedUrl = apiService.url + 'GetGamePlanets/G-000000000001';

    httpMock.post.mockReturnValue(of('success'));

    return apiService.GetGamePlanets().toPromise().then(response => {
      expect(response).toEqual('success');
      expect(httpMock.post).toHaveBeenCalledWith(expectedUrl, {});
    });
  });

  it('should handle errors when getting game planets', () => {
    const expectedUrl = apiService.url + 'GetGamePlanets/G-000000000001'; //Game does not exists

    httpMock.post.mockReturnValue(throwError('error'));

    return apiService.GetGamePlanets().toPromise().catch(error => {
      expect(error).toBe('error');
      expect(httpMock.post).toHaveBeenCalledWith(expectedUrl, {});
    });
  });
});

import { gameService } from '../app/components/services/Game.service';
import { HttpClient } from '@angular/common/http';
import { of, throwError } from 'rxjs';

describe('ApiService', () => {
  let apiService: gameService;
  let httpMock: any;

  beforeEach(() => {
    httpMock = {
      post: jest.fn()
    };

    apiService = new gameService(httpMock);
  });

  it('should send a POST request to draw a card', () => {
    const playerID: string = 'U-01a1zjlg4aph';

    const expectedUrl = apiService.url + 'DrawCard/' + apiService.getgameID() + '/' + playerID;

    httpMock.post.mockReturnValue(of('success'));

    return apiService.drawCard(playerID).toPromise().then(response => {
      expect(response).toEqual('success');
      expect(httpMock.post).toHaveBeenCalledWith(expectedUrl, {});
    });
  });

  it('should handle errors when drawing a card', () => {
    const playerID: string = 'U-01a1zjlg4apZ'; //Player does not exist

    const expectedUrl = apiService.url + 'DrawCard/' + apiService.getgameID() + '/' + playerID;

    httpMock.post.mockReturnValue(throwError('error'));

    return apiService.drawCard(playerID).toPromise().catch(error => {
      expect(error).toBe('error');
      expect(httpMock.post).toHaveBeenCalledWith(expectedUrl, {});
    });
  });
});

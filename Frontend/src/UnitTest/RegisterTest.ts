import { LoginService } from '../app/components/services/login.service';
import { AccountInt} from '../app/components/interfaces/account.interface';
import { CountryInterface} from '../app/components/interfaces/countryinterface';
import { of, throwError } from 'rxjs';


describe('ApiService', () => {
  let apiService: LoginService;
  let httpMock: any;
  let formServiceMock: any;

  beforeEach(() => {
    httpMock = {
      post: jest.fn()
    };

    formServiceMock = {
      searchcountryID: jest.fn()
    };

    apiService = new LoginService(httpMock, formServiceMock);
  });

  it('should send a POST request to register an account', () => {
    const player: AccountInt = {
      id: null,
      email: 'test@example.com',
      firstName: 'John',
      lastName: 'Doe',
      username: 'johndoe',
      pHash: '1111qqqq',
      level: 1,
      ranking: null,
      country: 'Costa Rica',
      coins: 0,
      avatar: 'avatar.jpg'
    };
    const countries: CountryInterface[] = [{ id: '1', countryName: 'Costa Rica' }];

    const expectedPlayerForAPI = {
      email: player.email,
      firstName: player.firstName,
      lastName: player.lastName,
      username: player.username,
      password: player.pHash,
      avatar: player.avatar,
      countryId: '1' 
    };

    formServiceMock.searchcountryID.mockReturnValue(expectedPlayerForAPI.countryId);
    httpMock.post.mockReturnValue(of('success'));

    return apiService.registerAccount(player, countries).toPromise().then(response => {
      expect(response).toEqual('success');
      expect(httpMock.post).toHaveBeenCalledWith(apiService.url + 'Player/AddPlayer', expectedPlayerForAPI);
      expect(formServiceMock.searchcountryID).toHaveBeenCalledWith(player.country, countries);
    });
  });

  it('should handle errors when registering an account', () => {
    const player: AccountInt = {
      id: null,
      email: 'nasser@gmail,com',
      firstName: 'John',
      lastName: 'Doe',
      username: 'johndoe',
      pHash: '1111qqqq',
      level: 1,
      ranking: null,
      country: 'Costa Rica',
      coins: 0,
      avatar: 'avatar.jpg'
    };
    const countries: CountryInterface[] = [{ id: '1', countryName: 'Costa Rica' }];

    const expectedPlayerForAPI = {
      email: player.email,
      firstName: player.firstName,
      lastName: player.lastName,
      username: player.username,
      password: player.pHash,
      avatar: player.avatar,
      countryId: '1' 
    };

    formServiceMock.searchcountryID.mockReturnValue(expectedPlayerForAPI.countryId);
    httpMock.post.mockReturnValue(throwError('error'));

    return apiService.registerAccount(player, countries).toPromise().catch(error => {
      expect(error).toBe('error');
      expect(httpMock.post).toHaveBeenCalledWith(apiService.url + 'Player/AddPlayer', expectedPlayerForAPI);
      expect(formServiceMock.searchcountryID).toHaveBeenCalledWith(player.country, countries);
    });
  });
});

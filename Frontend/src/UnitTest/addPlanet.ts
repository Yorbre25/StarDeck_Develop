import { planetService } from '../app/components/services/Planet.service';
import { PlanetInterface} from '../app/components/interfaces/planet.interface';
import { TypeInterface} from '../app/components/interfaces/type.interface';
import { HttpClient } from '@angular/common/http';
import { of, throwError } from 'rxjs';

describe('ApiService', () => {
  let apiService: planetService;
  let httpMock: any;
  let formServiceMock: any;

  beforeEach(() => {
    httpMock = {
      post: jest.fn()
    };

    formServiceMock = {
      searchtypeID: jest.fn()
    };

    apiService = new planetService(httpMock, formServiceMock);
  });

  it('should send a POST request to add a planet', () => {
    const planet: PlanetInterface = {
      id: null,
      name: 'Earth',
      image: 'earth.jpg',
      description: 'The third planet from the Sun.',
      activated_planet: null,
      type: 'Basico',
      show: null
    };
    const types: TypeInterface[] = [{ id: '1', typeName: 'Basico' }];

    const expectedPlanetForAPI = {
      name: planet.name,
      typeId: '1', // Assuming the searchtypeID method returns the type ID for 'Terrestrial'
      description: planet.description,
      image: planet.image
    };

    formServiceMock.searchtypeID.mockReturnValue(expectedPlanetForAPI.typeId);
    httpMock.post.mockReturnValue(of('success'));

    return apiService.addPlanet(planet, types).toPromise().then(response => {
      expect(response).toEqual('success');
      expect(httpMock.post).toHaveBeenCalledWith(apiService.url + 'Planet/AddPlanet', expectedPlanetForAPI);
      expect(formServiceMock.searchtypeID).toHaveBeenCalledWith(planet.type, types);
    });
  });

  it('should handle errors when adding a planet', () => {
    const planet: PlanetInterface = {
      id: null,
      name: 'Planeta B',
      image: 'earth.jpg',
      description: 'The third planet from the Sun.',
      activated_planet: null,
      type: 'Basico',
      show: null
    };
    const types: TypeInterface[] = [{ id: '1', typeName: 'Basico' }];

    const expectedPlanetForAPI = {
      name: planet.name,
      typeId: '1', 
      description: planet.description,
      image: planet.image
    };

    formServiceMock.searchtypeID.mockReturnValue(expectedPlanetForAPI.typeId);
    httpMock.post.mockReturnValue(throwError('error'));

    return apiService.addPlanet(planet, types).toPromise().catch(error => {
      expect(error).toBe('error');
      expect(httpMock.post).toHaveBeenCalledWith(apiService.url + 'Planet/AddPlanet', expectedPlanetForAPI);
      expect(formServiceMock.searchtypeID).toHaveBeenCalledWith(planet.type, types);
    });
  });
});

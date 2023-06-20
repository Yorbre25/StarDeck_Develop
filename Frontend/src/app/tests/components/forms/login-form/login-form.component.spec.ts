import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'

import { LoginFormComponent } from '../../../../components/forms/login-form/login-form.component';
import { LoginService } from 'src/app/components/services/login.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { LobbyComponent } from 'src/app/components/views/lobby/lobby.component';
import { Observable, observable, of } from 'rxjs';
import { AccountInt } from 'src/app/components/interfaces/account.interface';

describe('LoginFormComponent', () => {
  let component: LoginFormComponent;
  let fixture: ComponentFixture<LoginFormComponent>;
  let compiled: HTMLElement;
  let service: LoginService;
  let httpMock: HttpTestingController;
  let router: Router;

  const mockLoginService: {
    Login: () => Observable<any>,
    getAllPlayers: () => Observable<AccountInt[]>,
    setcorreo: () => null,
    searchPlayerID: () => string,
    setid: () => null,
    setloggedinpl: () => null
  } = {
    Login: () => of(),
    // poner que retorna cada funcion del mock
  }
  
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginFormComponent ],

      imports: [
        HttpClientTestingModule,
        FormsModule,
        RouterTestingModule.withRoutes([{path: 'home', component: LobbyComponent}])
      ],
    providers: [
      { provide: LoginService, useValue: mockLoginService }
    ]
    })
    .compileComponents();
  });

    fixture = TestBed.createComponent(LoginFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

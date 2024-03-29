import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { AuthfakeauthenticationService } from 'src/app/core/services/authfake.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertService } from 'src/app/core/services/alert.service';
import { LoginService } from 'src/app/shared/services/login.service';
import { Login } from 'src/app/core/models/login.models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  loginForm: UntypedFormGroup;
  error = '';
  alerts: any = [];

  constructor(
  private spinner: NgxSpinnerService,
  private formBuilder: UntypedFormBuilder, 
  private router: Router, 
  public translate: TranslateService,
  public loginService: LoginService,
  private alertService: AlertService,
  private authFackservice: AuthfakeauthenticationService,
  ) {
    this.translate.addLangs(['pt', 'es']);
    this.translate.setDefaultLang('pt');
    const browserLang = this.translate.getBrowserLang();
    this.translate.use(browserLang.match(/pt|es/) ? browserLang : 'pt');
  }

  ngOnInit() {    
    this.createFormLogin(new Login());
  }

  createFormLogin(login: Login) {
    this.loginForm = this.formBuilder.group({
      email: [login.email, [Validators.required, Validators.pattern('^([\\w\\.\\-]+)@([\\w]+)\\.([\\w\\.]+)$'), Validators.minLength(11)]],
      password: [login.password, Validators.required],
    });
  }

  login() {
    if(this.loginForm.invalid){
      this.alerts.push(this.alertService.errorIcon(this.translate.instant('LOGIN.ERROR')));
    } 
    else {    
      this.spinner.show();
      this.loginService.login(this.loginForm.value).subscribe(
        (response) => {          
          if(response.data != null) {                   
            let user = response.data[0];
            this.authFackservice.login(user);
            this.router.navigate(['/transaction']);
          }
          else {
            if(response.isSuccessful == false) {              
              this.alerts.push(this.alertService.errorIcon(this.translate.instant(response.messageCode)));
            }
          }
        },
        (error) => {          
          this.alerts.push(this.alertService.errorIcon(this.translate.instant('LOGIN.ERROR')));
        }
      ).add(() => {
        this.spinner.hide();
      })
    }
  }
}

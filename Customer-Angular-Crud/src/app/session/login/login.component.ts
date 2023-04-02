
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { SessionService } from '../services/session.services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private authService: SessionService, private router: Router) {
    this.loginForm = new FormGroup({
        email: new FormControl(''),
        password: new FormControl('')
      });
   }

  ngOnInit() {
    
  }

  submit() {
    if (this.loginForm.valid) {
      this.authService.login().subscribe(() => {
        this.router.navigateByUrl('customers');
      })
    }
  }
}

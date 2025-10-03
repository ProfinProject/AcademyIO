import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth/services/auth.service';
import { LoginCredentials } from '../../core/auth/models/auth.interfaces';
import { AccountService } from './account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  providers: [AccountService], 
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class Login {
  credentials: LoginCredentials = {
    email: '',
    password: ''
  };
  errorMessage = '';
  isLoading = false;

  constructor(
    private authService: AuthService,
    private accountService: AccountService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  onSubmit(): void {
    this.errorMessage = '';
    this.isLoading = true;
    this.authService.login(this.credentials).subscribe({
      next: (response) => {
        this.isLoading = false;
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
  
        this.accountService.LocalStorage.saveLocalDataUser(response);

        if(this.accountService.LocalStorage.isAdmin())
          this.router.navigateByUrl(returnUrl || '/painel-administrador');
        else
          this.router.navigateByUrl(returnUrl || '/home');
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Login error:', err);
        // A mensagem de erro agora vem diretamente do serviço.
        if (err instanceof Error) {
          this.errorMessage = err.message;
        } else {
          this.errorMessage = 'Ocorreu um erro inesperado. Tente novamente.';
        }

      }
    });
  }
}

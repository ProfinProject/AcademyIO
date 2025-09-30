import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth/services/auth.service';
import { RegisterCredentials } from '../../core/auth/models/auth.interfaces';

@Component({
  selector: 'app-registrar',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './registrar.html',
  styleUrl: './registrar.css'
})
export class Registrar {
  credentials: RegisterCredentials = {
    firstName: '',
    lastName: '',
    email: '',
    dateOfBirth: '',
    password: '',
    confirmPassword: ''
  };
  errorMessage = '';
  successMessage = '';
  passwordMismatch = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  onSubmit(): void {
    this.errorMessage = '';
    this.successMessage = '';
    this.passwordMismatch = this.credentials.password !== this.credentials.confirmPassword;

    if (this.passwordMismatch) {
      this.errorMessage = 'As senhas não coincidem.';
      return;
    }

    this.authService.register(this.credentials).subscribe({
      next: () => {
        this.successMessage = 'Cadastro realizado com sucesso! Redirecionando para o login...';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Ocorreu um erro ao tentar se registrar. Tente novamente.';
      }
    });
  }
}

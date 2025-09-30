import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth/services/auth.service';
import { LoginCredentials } from '../../core/auth/models/auth.interfaces';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
    private router: Router,
    private route: ActivatedRoute
  ) { }

  onSubmit(): void {
    this.errorMessage = '';
    this.isLoading = true;
    this.authService.login(this.credentials).subscribe({
      next: () => {
        this.isLoading = false;
        // Verifica se há
        //  uma URL de retorno nos parâmetros da rota
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
        // Redireciona para a URL de retorno ou para o painel do administrador como padrão
        this.router.navigateByUrl(returnUrl || '/painel-administrador');

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

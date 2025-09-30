import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrar',
  imports: [ReactiveFormsModule, HttpClientModule],
  templateUrl: './registrar.html',
  styleUrl: './registrar.css'
})
export class Registrar {
loginForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.loginForm = this.fb.group({
      nome: [''],
      email: [''],
      senha: [''],
      confirmarSenha: ['']
    });
  }

  onSubmit() {
    const loginData = this.loginForm.value;
    this.http.post('https://localhost:7234/api/auth/new-account', loginData)
      .subscribe({
        next: (response: any) => {
          localStorage.setItem('accessToken', response.accessToken);
          this.router.navigate(['/painel-administrador']);
          console.log('Cadastro realizado com sucesso!');
        },
        error: (err) => {
          console.error('Erro ao fazer cadastro:', err);
        }
      });
  }
}

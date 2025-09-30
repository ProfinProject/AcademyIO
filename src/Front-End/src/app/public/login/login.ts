import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, HttpClientModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  loginForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.loginForm = this.fb.group({
      email: [''],
      password: ['']
    });
  }

  onSubmit() {
    const loginData = this.loginForm.value;
    this.http.post('https://localhost:7234/api/auth/auth', loginData)
      .subscribe({
        next: (response: any) => {
          localStorage.setItem('accessToken', response.accessToken);
          this.router.navigate(['/painel-administrador'])
          console.log('Login realizado com sucesso!');
        },
        error: (err) => {
          console.error('Erro ao fazer login:', err);
        }
      });
  }
}


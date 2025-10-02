import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, tap, catchError, throwError, timeout, map } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
import { LoginCredentials, LoginResponse, RegisterCredentials } from '../models/auth.interfaces';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7234/api/auth';
  private readonly TOKEN_KEY = 'authToken';

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  /**
   * Envia as credenciais de login para o backend.
   * @param credentials - Objeto com email e senha do usuário.
   * @returns Um Observable com a resposta do backend.
   */
  login(credentials: LoginCredentials): Observable<LoginResponse> {
    const httpOptions = {
      headers: new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('Accept', '*/*'),
      observe: 'response' as const
    };
    console.log('Login response:entrei no login');
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/auth`,
      credentials,
      httpOptions
    ).pipe(
      timeout(50000), // 50 second timeout
      tap((response) => {
        console.log('Login response:', response);
        if (response?.body?.accessToken) {
          this.saveToken(response.body.accessToken);
        }
      }),
      map(response => response.body as LoginResponse),
      catchError(error => this.handleError(error))
    );
  }

  /**
   * Envia os dados de registro para o backend.
   * @param credentials - Objeto com os dados do novo usuário.
   * @returns Um Observable com a resposta do backend.
   */
  register(credentials: RegisterCredentials): Observable<any> {
    return this.http.post(`${this.apiUrl}/new-account`, credentials);
  }

  /**
   * Salva o token de autenticação no localStorage.
   * @param token - O token JWT recebido do backend.
   */
  private saveToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem(this.TOKEN_KEY, token);
    }
  }

  /**
   * Recupera o token de autenticação do localStorage.
   * @returns O token ou null se não existir.
   */
  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem(this.TOKEN_KEY);
    }
    return null;
  }

  /**
   * Verifica se o usuário está autenticado.
   * @returns `true` se houver um token, `false` caso contrário.
   */
  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!this.getToken();
    }
    return false;
  }

  /**
   * Remove o token de autenticação para fazer logout.
   */
  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.TOKEN_KEY);
    }
    // Aqui você pode redirecionar o usuário para a página de login.
  }

  /**
   * Trata os erros da requisição HTTP.
   * @param error - O erro retornado pela requisição HTTP.
   * @returns Um Observable com o erro tratado.
   */
  private handleError(error: HttpErrorResponse) {
    // Tenta extrair a mensagem de erro do corpo da resposta da API.
    // Muitas APIs (incluindo ASP.NET Core) retornam um objeto com uma propriedade 'title' ou 'message'.
    let errorMessage = error.error?.title || error.error?.message || error.message;

    if (error.status === 0) {
      errorMessage = 'Unable to connect to the server. Please check if the API is running and accessible.';
    } else if (error.status === 401) {
      // Para 401, uma mensagem específica é mais apropriada.
      errorMessage = 'Credenciais inválidas. Verifique seu e-mail e senha.';
    } else if (error.status === 400) {
      // Para 400, a mensagem extraída do corpo do erro costuma ser a mais útil.
      // Se não houver uma mensagem específica, usamos um texto padrão.
      errorMessage = error.error.errors.Messages.join(' | ');
    } else if (error.error instanceof ErrorEvent) {
      errorMessage = `Client error: ${error.error.message}`;
    }

    console.error('Auth error:', error);
    // Retorna um observable que emite o objeto de erro para o componente poder inspecioná-lo.
    return throwError(() => new Error(errorMessage)); // Garante que sempre retornamos um objeto Error
  }
}

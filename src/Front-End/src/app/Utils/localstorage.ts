import { isPlatformBrowser } from '@angular/common';
import { Inject, PLATFORM_ID } from '@angular/core';

export class LocalStorageUtils {
  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}

  // Checa se estamos no navegador
  private get isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  public getUser(): any | null {
    if (!this.isBrowser) return null;

    try {
      const user = localStorage.getItem('academyio.user');
      return user ? JSON.parse(user) : null;
    } catch (error) {
      console.error('Erro ao recuperar usuário:', error);
      return null;
    }
  }

  public isAdmin(): boolean {
    const user = this.getUser();
    if (!user || !user.claims) return false;

    const roleClaim = user.claims.find(
      (c: { type: string; value: string }) =>
        c.type === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    );
    return roleClaim?.value === 'ADMIN';
  }

  public saveLocalDataUser(response: any) {
    this.saveUserToken(response.accessToken);
    this.saveUser(response.userToken);
  }

  public cleanLocalDataUser() {
    if (!this.isBrowser) return;
    localStorage.removeItem('academyio.token');
    localStorage.removeItem('academyio.user');
  }

  public getUserToken(): string {
    if (!this.isBrowser) return '';
    const token = localStorage.getItem('academyio.token');
    return token ?? '';
  }

  public saveUserToken(token: string) {
    if (!this.isBrowser) return;
    localStorage.setItem('academyio.token', token);
  }

  public saveUser(user: string) {
    if (!this.isBrowser) return;
    localStorage.setItem('academyio.user', JSON.stringify(user));
  }
}

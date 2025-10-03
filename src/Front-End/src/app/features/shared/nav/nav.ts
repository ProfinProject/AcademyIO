import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { CommonModule } from '@angular/common';
import { isPlatformBrowser } from '@angular/common';
import { LocalStorageUtils } from '../../../Utils/localstorage';


@Component({
  selector: 'app-nav',
  imports: [CommonModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
    private localStorageUtils: LocalStorageUtils;

    constructor(@Inject(PLATFORM_ID) private platformId: Object) {
      this.localStorageUtils = new LocalStorageUtils(this.platformId);
    }

    get isLoggedIn(): boolean {
      return !!this.localStorageUtils.getUser();
    }

    get isAdmin(): boolean {
      return this.localStorageUtils.isAdmin();
    }

    get userEmail(): string {
      return this.localStorageUtils.getUser()?.email || '';
    }

    logout(): void {
      this.localStorageUtils.cleanLocalDataUser();
      window.location.href = '/'; 
    }

 // Função para hover seguro
  onHover(event: Event, hover: boolean) {
    const target = event.target as HTMLElement | null;
    if (!target) return;

    if (hover) {
      target.style.boxShadow = '0 4px 10px rgba(2, 161, 2, 0.25)';
      target.style.transform = 'translateY(-2px)';
    } else {
      target.style.boxShadow = '0 2px 5px rgba(2, 161, 2, 0.15)';
      target.style.transform = 'translateY(0)';
    }
  }

  onHoverGradient(event: Event, hover: boolean) {
    const target = event.target as HTMLElement | null;
    if (!target) return;

    if (hover) {
      target.style.boxShadow = '0 5px 15px rgba(2, 161, 2, 0.4)';
      target.style.transform = 'translateY(-2px)';
    } else {
      target.style.boxShadow = '0 3px 8px rgba(2, 161, 2, 0.3)';
      target.style.transform = 'translateY(0)';
    }
  }
}

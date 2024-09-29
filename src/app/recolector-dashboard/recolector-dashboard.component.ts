import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SvgsComponent } from "../svgs/svgs.component";
import { ModuleComponent } from '../pages/module/module.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-recolector-dashboard',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, ModuleComponent, SvgsComponent],
  templateUrl: './recolector-dashboard.component.html',
  styleUrl: './recolector-dashboard.component.css'
})
export class RecolectorDashboardComponent {
  private openDropdown: string | null = null;

  toggleDropdown(menu: string) {
    this.openDropdown = this.openDropdown === menu ? null : menu;
  }

  isDropdownOpen(menu: string): boolean {
    return this.openDropdown === menu;
  }

  constructor(private router: Router) {}
  logout() {
    localStorage.clear(); 
    this.router.navigate(['/login']);
  }
}

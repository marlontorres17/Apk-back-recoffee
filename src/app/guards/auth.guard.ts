// auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  if (typeof window !== 'undefined') {
    const isLogged = localStorage.getItem('isLogged') === 'true';
    const roleId = localStorage.getItem('roleId');

    if (!isLogged) {
      console.log('No está logueado, redirigiendo a /login');
      router.navigate(['/login']);
      return false;
    }
    
    // Permitir acceso solo si el rol es 'admin' para el admin-dashboard
    if (route.url[0]?.path === 'admin-dashboard' && roleId !== 'admin') {
      console.log('Rol no autorizado, redirigiendo a /login');
      router.navigate(['/login']);
      return false;
    }

    if (route.url[0]?.path === 'recolector-dashboard' && roleId !== 'recolector') {
      console.log('Rol no autorizado, redirigiendo a /login');
      router.navigate(['/login']);
      return false;
    }
    
  } else {
    router.navigate(['/login']);
    return false;
  }

  return true;
};

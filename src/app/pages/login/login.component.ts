// login.component.ts
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, RouterLink, RouterLinkActive]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      const isLogged = localStorage.getItem('isLogged') === 'true';
      const roleId = localStorage.getItem('roleId');
  
      // Redirigir al dashboard correspondiente basado en el rol
      if (isLogged) {
        if (roleId === 'admin') {
          this.router.navigate(['/admin-dashboard']);
        } else {
          this.router.navigate(['/recolector-dashboard']);
        }
      }
    }
  }
  

  login() {
    if (this.loginForm.invalid) {
      return;
    }
    

    const loginPayload = this.loginForm.value;

    this.http.post<any>('http://localhost:9191/api/Login/login', loginPayload)
      .subscribe(
        response => {
          const user = response.user;
          const roles = response.roles || [];

          if (user && roles.length > 0) {
            localStorage.setItem('isLogged', 'true');
            localStorage.setItem('roleId', roles[0]); // Asumiendo que el primer rol es el rol principal

            // Redirigir basado en el rol
            if (roles.includes('admin')) {
              this.router.navigate(['/admin-dashboard']);
            } else if (roles.includes('recolector')) {
              this.router.navigate(['/recolector-dashboard']);
            } else {
              this.errorMessage = 'Rol no reconocido.';
            }
            console.log('isLogged:', localStorage.getItem('isLogged'));
console.log('roleId:', localStorage.getItem('roleId'));

          } else {
            this.errorMessage = 'Información de usuario incorrecta.';
          }
        },
        error => {
          this.errorMessage = 'Nombre de usuario o contraseña incorrectos.';
        }
      );
  }
}

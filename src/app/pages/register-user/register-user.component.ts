import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register-user',
  standalone: true,
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css'],
  imports: [CommonModule, FormsModule, RouterModule, HttpClientModule, NgbModule],
})
export class RegisterUserComponent implements OnInit {
  user = {
    id: 0,
    state: true,
    userName: '',
    password: '',
    personId: 0
  };

  person = {
    id: 0,
    state: true,
    firstName: '',
    secondName: '',
    firstLastName: '',
    secondLastName: '',
    email: '',
    dateOfBirth: '',
    gender: '',
    cityId: 0,
    typeDocument: '',
    numberDocument: ''
  };

  roleId = 0;
  cities: any[] = [];
  roles: any[] = [];
  showLoginButton = false; // Control para mostrar el botón de iniciar sesión

  private userApiUrl = 'http://localhost:9191/api/User/register';
  private cityApiUrl = 'http://localhost:9191/api/City';
  private roleApiUrl = 'http://localhost:9191/api/Role';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef, private router: Router) {}

  ngOnInit(): void {
    this.getCities();
    this.getRoles();
    this.enableExternalScroll();  // Asegúrate de habilitar el desplazamiento externo aquí
  }

  getCities(): void {
    this.http.get<any[]>(this.cityApiUrl).subscribe(
      (cities) => {
        this.cities = cities;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching cities:', error);
      }
    );
  }

  getRoles(): void {
    this.http.get<any[]>(this.roleApiUrl).subscribe(
      (roles) => {
        this.roles = roles;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching roles:', error);
      }
    );
  }

  togglePasswordVisibility() {
    const passwordInput = document.getElementById('password') as HTMLInputElement;
    passwordInput.type = passwordInput.type === 'password' ? 'text' : 'password';
  }

  onSubmit(form: NgForm): void {
    const payload = {
      user: this.user,
      person: this.person,
      roleId: this.roleId
    };

    this.http.post(this.userApiUrl, payload).subscribe(() => {
      Swal.fire('Success', 'User registered successfully', 'success');
      this.showLoginButton = true; // Mostrar el botón de iniciar sesión
      form.resetForm();
      this.resetForm();
    }, (error) => {
      Swal.fire('Error', 'Failed to register user', 'error');
      console.error('Error registering user:', error);
    });
  }

  private resetForm(): void {
    this.user = { id: 0, state: true, userName: '', password: '', personId: 0 };
    this.person = {
      id: 0, state: true, firstName: '', secondName: '', firstLastName: '',
      secondLastName: '', email: '', dateOfBirth: '', gender: '', cityId: 0, typeDocument: '', numberDocument: ''
    };
    this.roleId = 0;
  }

  // Método para redirigir al componente de inicio de sesión
  navigateToLogin(): void {
    this.router.navigate(['/login']);
  }

  // Método para habilitar el desplazamiento externo
  enableExternalScroll(): void {
    const registerContainer = document.querySelector('.register-container') as HTMLElement;

    document.body.addEventListener('wheel', (event) => {
      // Verifica si el cursor está fuera del contenedor de registro
      if (!registerContainer.contains(event.target as Node)) {
        event.preventDefault();  // Previene el desplazamiento del body
        registerContainer.scrollTop += event.deltaY;  // Desplaza el contenedor
      }
    });
  }
}

import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-join-farm',
  standalone: true,
  templateUrl: './join-farm.component.html',
  styleUrls: ['./join-farm.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    NgbModule
  ],
})
export class JoinFarmComponent {
  formData: any = {
    identificationNumber: '',
    codigoUnico: ''
  };

  private apiUrl = 'http://localhost:9191/api/JoinFarmPerson/join';

  constructor(private http: HttpClient) {}

  onSubmit(): void {
    // Validar campos
    if (!this.formData.identificationNumber || !this.formData.codigoUnico) {
      alert('Número de identificación y código de finca son requeridos.');
      return;
    }

    this.http.post(this.apiUrl, this.formData, { observe: 'response', responseType: 'text' }).subscribe(
      (response) => {
        console.log('Respuesta completa:', response); // Registro completo de la respuesta para depuración
        // Manejo de respuestas según el estado
        if (response.status === 200) {
          alert(response.body); // Utiliza el cuerpo de la respuesta como mensaje de éxito
        } else if (response.status === 204) {
          alert('Te has unido a la finca exitosamente pero sin contenido adicional.');
        } else {
          alert('Se recibió una respuesta inesperada del servidor.');
        }
      },
      (error: any) => {
        console.error('Error:', error); // Registro del error en la consola
        // Manejo de errores basado en la respuesta del servidor
        let errorMessage = 'Ocurrió un error inesperado. Por favor, intenta nuevamente.';
        if (error.status === 400) {
          errorMessage = error.error || 'Los datos proporcionados son incorrectos.';
        } else if (error.status === 404) {
          errorMessage = 'No se encontró la finca o la persona.';
        } else if (error.status === 409) {
          errorMessage = 'Ya estás unido a esta finca.';
        }

        alert('Error: ' + errorMessage); // Muestra mensaje de error
      }
    );
  }
}

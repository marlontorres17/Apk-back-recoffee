import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import 'datatables.net';


@Component({
  selector: 'app-person',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    NgbModule
  ],
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit, OnDestroy {
  persons: any[] = [];
  person: any = { id: 0, firstName: '', secondName: '', firstLastName: '', secondLastName: '', email: '', dateOfBirth: '', gender: '', cityId: 0, typeDocument: 'CC', numberDocument: '', state: true };
  cities: any[] = [];
  filteredCities: any[] = [];
  selectedCity = '';
  closeResult = '';
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject(); // Inicialización del Subject

  private personApiUrl = 'http://localhost:9191/api/Person';
  private citiesApiUrl = 'http://localhost:9191/api/City';

  @ViewChild('personModal') personModal!: TemplateRef<any>;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.getPersons();
    this.getCities();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      processing: true,
    };
  }

  getPersons(): void {
    this.http.get<any[]>(this.personApiUrl).subscribe(
      (persons) => {
        this.persons = persons;
        this.cdr.detectChanges();
        this.dtTrigger.next(null); // Activar el DataTable después de obtener los datos
        console.log('Persons fetched:', this.persons);
      },
      (error) => {
        console.error('Error fetching persons:', error);
      }
    );
  }

  getCities(): void {
    this.http.get<any[]>(this.citiesApiUrl).subscribe(
      (cities) => {
        this.cities = cities;
        this.filteredCities = [...this.cities];
        this.cdr.detectChanges();
        console.log('Cities fetched:', this.cities);
      },
      (error) => {
        console.error('Error fetching cities:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    const personDto = { ...this.person };

    if (this.person.id === 0) {
      this.http.post(this.personApiUrl, personDto).subscribe(() => {
        this.getPersons();
        this.resetForm();
        Swal.fire('Success', 'Person created successfully', 'success');
      });
    } else {
      this.http.put(`${this.personApiUrl}/${this.person.id}`, personDto).subscribe(() => {
        this.getPersons();
        this.resetForm();
        Swal.fire('Success', 'Person updated successfully', 'success');
      });
    }
  }

  resetForm(): void {
    this.person = { id: 0, firstName: '', secondName: '', firstLastName: '', secondLastName: '', email: '', dateOfBirth: '', gender: '', cityId: 0, typeDocument: 'CC', numberDocument: '', state: true };
    this.selectedCity = '';
  }

  openModal(): void {
    this.resetForm();
    this.modalService.open(this.personModal, { ariaLabelledBy: 'modal-basic-title' });
  }

  editPerson(person: any): void {
    this.person = { ...person };
    this.selectedCity = this.getCityName(person.cityId);
    this.modalService.open(this.personModal, { ariaLabelledBy: 'modal-basic-title' });
  }

  deletePerson(id: number, type: string): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(`${this.personApiUrl}/${id}`).subscribe(() => {
          this.getPersons();
          Swal.fire('Deleted!', 'The person has been deleted.', 'success');
        });
      }
    });
  }

  getCityName(cityId: number): string {
    const city = this.cities.find(c => c.id === cityId);
    return city ? city.name : 'Unknown';
  }

  filterCities(): void {
    this.filteredCities = this.cities.filter(city => city.name.toLowerCase().includes(this.selectedCity.toLowerCase()));
  }

  selectCity(city: any): void {
    this.selectedCity = city.name;
    this.person.cityId = city.id;
    this.filteredCities = [];
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe(); // Asegúrate de limpiar el Subject
  }
}

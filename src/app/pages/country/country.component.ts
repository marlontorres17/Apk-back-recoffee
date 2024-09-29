import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { NgbModal, ModalDismissReasons, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    NgbModule
  ],
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  countries: any[] = [];
  country: any = { id: 0, name: '', code: '', description: '', state: true };
  closeResult = '';
  private apiUrl = 'http://localhost:9191/api/Country';

  @ViewChild('countryModal') countryModal!: TemplateRef<any>;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef, private modalService: NgbModal) {}

  ngOnInit(): void {
    this.getCountries();
  }

  getCountries(): void {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (countries) => {
        this.countries = countries;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching countries:', error);
      }
    );
  }

  onSubmit(form: NgForm): void {
    const countryDto = { ...this.country };

    if (this.country.id === 0) {
      this.http.post(this.apiUrl, countryDto).subscribe(() => {
        this.getCountries();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Country created successfully', 'success');
      });
    } else {
      this.http.put(`${this.apiUrl}/${this.country.id}`, countryDto).subscribe(() => {
        this.getCountries();
        form.resetForm();
        this.resetForm();
        Swal.fire('Success', 'Country updated successfully', 'success');
      });
    }
  }

  openModal(country: any = { id: 0, name: '', code: '', description: '', state: true }): void {
    this.country = { ...country };
    this.modalService.open(this.countryModal).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  editCountry(country: any): void {
    this.openModal(country);
  }

  deleteCountry(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You won\'t be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
          this.getCountries();
          Swal.fire('Deleted!', 'Country has been deleted.', 'success');
        });
      }
    });
  }

  private resetForm(): void {
    this.country = { id: 0, name: '', code: '', description: '', state: true };
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}

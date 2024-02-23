import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-pdf-export',
  templateUrl: './pdf-export.component.html',
  styleUrl: './pdf-export.component.css',
})
export class PdfExportComponent {
  //Service In angular used when there is common property used to share among multiple compunent
  // Also API Calling is done from Service

  // Injecting HttpHttpClient
  constructor(private http: HttpClient) { }

  convertToPdf(url: string): Observable<Blob> {
    return this.http.get(`https://localhost:7105/api/Pdf?url=${encodeURIComponent(url)}`, {
      responseType: 'blob'
    });
  }
}

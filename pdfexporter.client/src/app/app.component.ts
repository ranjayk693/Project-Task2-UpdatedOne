import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PdfExportComponent } from './service/pdf-export/pdf-export.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private pdfConversionService:PdfExportComponent){}
  //URL of the web page
  url: string = '';

  // Initally pdfBlob store null value after getting response it will store pdf
  pdfBlob: Blob | null = null;

  // Intially the spinner animation is hidden
  IsSpin = false;

  ngOnInit(): void {}

  // This function sent the url of webpage and save the pdf in pdfBlob
  convertToPdf() {
    // Spinner will start and also display in the webpage
    this.IsSpin = true;

    // Check if user enter the empty url
    if (!this.url) {
      alert('Please enter a URL.');
      this.IsSpin = false;
      return;
    }

    //Service Call 
    this.pdfConversionService.convertToPdf(this.url).subscribe(
      (response: Blob) => {
        this.pdfBlob = response;
        this.IsSpin = false;
      },
      (error) => {
        this.IsSpin = false;
        alert('Error converting to PDF. Please try again.');
      }
    );
  }

  // This will generate the PDF
  ExportPDF() {
    // Check for empty file in pdfBlob
    if (!this.pdfBlob) {
      alert('No PDF to export. Please convert to PDF first.');
      return;
    }
    // Automatically a tag will generate and pdf get downloaded from the client side
    const url = window.URL.createObjectURL(this.pdfBlob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'webpage.pdf';
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);

    // Clear the data after download the pdf
    this.pdfBlob=null;
  }
}

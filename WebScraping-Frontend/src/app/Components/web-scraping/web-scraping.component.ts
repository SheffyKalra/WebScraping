import { Component, EventEmitter, Output, ElementRef, ɵConsole, OnInit } from '@angular/core'
import { NgbModule, ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { WebScrapingService } from '../../Services/web-scraping.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import { FormGroup, FormControl, FormBuilder , Validators } from '@angular/forms';
import htmlToImage from 'html-to-image';

@Component({
  selector: 'app-web-scraping',
  templateUrl: './web-scraping.component.html',
  styleUrls: ['./web-scraping.component.css']
})
export class WebScrapingComponent implements OnInit {
  scrapForm: FormGroup;
  submitted: boolean = false;
  resultView: boolean = false;
  showMessage: boolean = false;
  showErrorMessage: boolean = false;
  ScrapedData: any;
  

  constructor(private formBuilder: FormBuilder,    private el: ElementRef, private modalService: NgbModal, private _webScrapingService: WebScrapingService) {
    const regname = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
    this.scrapForm = this.formBuilder.group({
      siteName: ['', [Validators.required, Validators.pattern(regname)]]

    });

  }



  DownloadPdf() {
    htmlToImage.toJpeg(document.getElementById('resultViewHtml'), { quality: 1 })
      .then(function (dataUrl) {
        var link = document.createElement('a');
        link.download = 'my-image-name.jpeg';
        link.href = dataUrl;
        link.click();
      });

   
  }
  onSubmit() {

    this.submitted = true;
    this.showErrorMessage = false;
    // stop here if form is invalid
    if (this.scrapForm.invalid) {
      
      return;
    }
    this.showMessage = true;
    this._webScrapingService.GetScrapedData(this.scrapForm.value.siteName).subscribe(
      response => {
        debugger;
        this.ScrapedData = response;
        this.showMessage = false;;
        if (this.ScrapedData !== null) {
          this.resultView = true;
          console.log(this.ScrapedData);
        }
        else {
          this.showErrorMessage = true;
        }
      },
      error => {
        Swal.fire({
          icon: 'error',
          title: 'Something went wrong!',
          text: 'Oops...',
          width: 500,
          padding: '4em',
        })
      }
    )
  }
  get f() {
    return this.scrapForm.controls;
  }
  get siteName() { return this.scrapForm.get('siteName') };

  onReset() {
    this.submitted = false;
    this.scrapForm.reset();
    this.showMessage = false;
    this.showErrorMessage = false;
    this.resultView = false;
  }
  ngOnInit(): void {
  }

}



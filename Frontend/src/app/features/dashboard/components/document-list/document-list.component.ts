import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ApiUrl } from 'src/app/api-url';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-document-list.component',
  standalone: false,
  templateUrl: './document-list.component.html'
})
export class DocumentList implements OnInit {

  listOfItems: any[] = [];
  selectedFile!: File;
   loading = false;
  constructor(
    private location: Location,
    private message: NzMessageService,
    private router: Router,
    private modal: NzModalService,
    private http: HttpClient,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadDocuments();
  }

  loadDocuments(): void {
     this.loading = true;
    this.http.get(ApiUrl.base_api_url + `/api/documents`)
      .subscribe({
        next: (res: any) => {
          this.listOfItems = res;
          this.loading = false;
          this.cd.detectChanges(); 
        },
        error: () => {
          this.message.error('Failed to load documents');
          this.loading = false;
          this.cd.detectChanges(); 
        }
      });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (!file) return;

    this.selectedFile = file;
    this.upload();
  }

  upload(): void {
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.http.post(ApiUrl.base_api_url + `/api/documents/upload`, formData)
      .subscribe({
        next: (res: any) => {

          this.message.success('Document uploaded successfully');

          this.router.navigate(['dashboard/edit'], {
            state: { data: res }
          });

          
          this.listOfItems = [res, ...this.listOfItems];
        },
        error: (err) => {
          const messages = err.error?.errors?.userError;

          if (messages && messages.length) {
            this.message.error(messages[0]);
          } else {
            this.message.error('Upload failed');
          }
        }
      });
  }
}
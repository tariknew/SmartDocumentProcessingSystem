import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Validators } from '@angular/forms';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { ApiUrl } from 'src/app/api-url';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-uploaded-document',
  standalone: false,
  templateUrl: './edit-uploaded-document.html',
  styleUrl: './edit-uploaded-document.less'
})
export class EditUploadedDocument implements OnInit {

  parsedData: any;
  validateForm!: FormGroup;
  

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private modal: NzModalService,
    private http: HttpClient,
  ) {}

ngOnInit(): void {
  const nav = history.state;
  
  this.parsedData = nav.data;

  if (!this.parsedData) {
    this.router.navigate(['/dashboard']);
    return;
  }

  this.initForm();

const docType = this.validateForm.get('documentType')?.value;

if (docType !== 'Invoice') {
  this.validateForm.get('dueDate')?.clearValidators();
  this.validateForm.get('dueDate')?.updateValueAndValidity();
}

  this.validateForm.markAllAsTouched();
  this.validateForm.updateValueAndValidity();

  const errors = [...(this.parsedData.errors || [])];
  
if (!this.parsedData.lineItems || this.parsedData.lineItems.length === 0) {
  errors.push({
    fieldName: 'LineItems',
    message: 'It needs at least one line item'
  });
}
  if (errors && errors.length) {
    setTimeout(() => this.showErrors(errors));
  }
}

  get lineItems(): FormArray {
    return this.validateForm.get('lineItems') as FormArray;
  }

  showErrors(errors: any[]) {

  

 

  this.modal.error({
    nzTitle: 'Validation errors',
    nzContent: `
      <ul style="padding-left:20px">
        ${errors.map(e => `<li><b>${e.fieldName}</b>: ${e.message}</li>`).join('')}
      </ul>
    `
  });
}

  initForm(): void {
    const data = this.parsedData;
    
    this.validateForm = this.fb.group({
      documentType: [this.setFieldValue(data.documentType), [Validators.required]],
      supplierName: [this.setFieldValue(data.supplierName), [Validators.required]],
      documentNumber: [this.setFieldValue(data.documentNumber), [Validators.required]],
      issueDate: [data.issueDate ? new Date(data.issueDate) : null, [Validators.required]],
      dueDate: [data.dueDate ? new Date(data.dueDate) : null, [Validators.required]],
      currency: [this.setFieldValue(data.currency), [Validators.required]],
      taxRate: [this.setFieldValue(data.taxRate), [Validators.required]],
      subtotal: [this.setFieldValue(data.subtotal),[Validators.required]],
      tax: [this.setFieldValue(data.tax), [Validators.required]],
      status: [data.status, [Validators.required]],
      total: [this.setFieldValue(data.total), [Validators.required]],
      lineItems: this.fb.array([])
    });

    if (data.lineItems) {
  data.lineItems.forEach((item: any) => {
    this.lineItems.push(
      this.fb.group({
        id: [item.id],
        documentId: [item.documentId],
        description: [this.setFieldValue(item.description), [Validators.required]],
        quantity: [this.setFieldValue(item.quantity), [Validators.required]],
        unitPrice: [this.setFieldValue(item.unitPrice), [Validators.required]],
        total: [this.setFieldValue(item.total), [Validators.required]]
      })
    );
  });
}
  }

  setFieldValue(value: any): number | null {
  if (value === 0 || value === '' || value === undefined) {
    return null;
  }
  return value;
}



  submitForm(): void {
  if (this.validateForm.invalid) {
    this.validateForm.markAllAsTouched();
    return;
  }




const payload = {
  ...this.validateForm.value,
  issueDate: this.formatDate(this.validateForm.value.issueDate),
  dueDate: this.formatDate(this.validateForm.value.dueDate)
};


  const id = this.parsedData.id; 

  this.http.put(ApiUrl.base_api_url + `/api/documents/${id}`, payload)
    .subscribe({
      next: () => {
        this.modal.success({
          nzTitle: 'Success',
          nzContent: 'Document updated successfully'
        });

        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.modal.error({
          nzTitle: 'Error',
          nzContent: 'Update failed'
        });
      }
    });
}
formatDate(date: Date): string {
  const d = new Date(date);
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
}
}
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DashboardRoutingModule } from './dashboard-routing-module';
import { DocumentList } from './components/document-list/document-list.component';
import { NgZorroModule } from '@modules/ng-zorro';
import { EditUploadedDocument } from './components/edit-uploaded-document/edit-uploaded-document';
import { CurrencyTotal } from './components/currency-total/currency-total';

@NgModule({
  declarations: [DocumentList, EditUploadedDocument, CurrencyTotal],
  imports: [CommonModule, DashboardRoutingModule, NgZorroModule, FormsModule, ReactiveFormsModule],
})
export class DashboardModule {}

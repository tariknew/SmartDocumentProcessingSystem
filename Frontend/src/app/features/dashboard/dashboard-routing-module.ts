import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocumentList } from './components/document-list/document-list.component';
import { EditUploadedDocument } from './components/edit-uploaded-document/edit-uploaded-document';
import { CurrencyTotal } from './components/currency-total/currency-total';

const routes: Routes = [
  { path: '', component: DocumentList },
  { path: 'edit', component: EditUploadedDocument },
  { path: 'currency-total', component: CurrencyTotal }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }

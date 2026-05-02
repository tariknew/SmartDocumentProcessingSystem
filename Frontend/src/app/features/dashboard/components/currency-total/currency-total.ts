import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { HttpClient } from '@angular/common/http';
import { ApiUrl } from 'src/app/api-url';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-currency-total',
  standalone: false,
  templateUrl: './currency-total.html',
  styleUrl: './currency-total.less',
})
export class CurrencyTotal implements OnInit {

  listOfItems: any[] = [];
   loading = false;

  constructor(
    private message: NzMessageService,
    private http: HttpClient,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
     this.loading = true;
    this.http.get(ApiUrl.base_api_url + `/api/documents/currency-total`)
      .subscribe({
        next: (res: any) => {
          this.listOfItems = res;
          this.loading = false;
          this.cd.detectChanges(); 
        },
        error: () => {
          this.message.error('Failed to load');
          this.loading = false;
          this.cd.detectChanges(); 
        }
      });
  }
}

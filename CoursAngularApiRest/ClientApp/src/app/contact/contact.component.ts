import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http"
@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  Contacts: any;
  constructor(private http: HttpClient, @Inject('BASE_URL') private  baseUrl: string) { }

  ngOnInit() {
    this.http.get(this.baseUrl + "/contact").subscribe((res) => {
      this.Contacts = res;
      console.dir(this.Contacts);
    })
  }

}

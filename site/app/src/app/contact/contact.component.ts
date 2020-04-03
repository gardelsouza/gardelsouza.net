import { Component, OnInit } from '@angular/core';
import { ContactService } from '../services/contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  constructor(private contactService: ContactService) { }

  text = "";

  ngOnInit(): void {
  }

  onSubmitTemplateBased() {
    var ret = this.contactService.sendMail(this.text);
    if(ret.message == "Email sent") {
      this.text = "";
    }
  }

}

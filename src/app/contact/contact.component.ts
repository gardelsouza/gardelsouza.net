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
    this.contactService.sendMail(this.text).subscribe(data => {
      if (data.message == "Email sent") {
        this.text = "";
        alert('Mensagem enviada, obrigado.')
      }
    }, error => {
      alert('Houve um probleminha ao enviar a mensagem, você pode tentar novamente ou entre em contato pelo email gardel@gardelsouza.net.')
    });
  }
}

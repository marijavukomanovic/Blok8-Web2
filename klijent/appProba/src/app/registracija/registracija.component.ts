import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators ,FormGroup} from '@angular/forms';
import { Router } from '@angular/router';
import {RegistracijaServis} from 'src/app/servisi/registracija.servis';
import {RegistracijaModel} from 'src/app/model/registracijaModel';
import {PassengerTypeEnum} from 'src/app/model/enums';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

user : RegistracijaModel = {
  Name : '',
  LastName : '',
  UserName : '',
  Email : '',
  Address : '',
  BirthdayDate : '',
  PassengerType : 1,
  Password : '',
  ConfirmPassword : '',
  Document : '',
};

registerForm = this.fb.group({
    Name : [''],
    LastName : [''],
    UserName : ['', Validators.required],
    Email : ['', Validators.required],
    Address : [''],
    BirthdayDate : [''],
    PassengerType : [''],
    Password : ['', Validators.required],
    ConfirmPassword : ['', Validators.required],
    Document : [''],
  });

  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  constructor(private fb : FormBuilder, private registracijaServis : RegistracijaServis, private router:Router) { }

  ngOnInit() {
    //this.user.PassengerType = PassengerType.Regularan;
  }

  onSubmit()
  {
    console.log(this.registerForm.value);
    this.registracijaServis.register(this.registerForm.value).subscribe(data => {
      console.log('Registration successfully done.');
      this.router.navigate(['/']);
    });
  }


  ownerLevels = [
    { id: 1, name: 'Student' },
    { id: 2, name: 'Penzioner' },
    { id: 3, name: 'Regularan' }
 ];
 
 selectedOwnerLevel: number = 0;
 
 onChangeOwnerLevel(ownerLevelId: number) {
    this.selectedOwnerLevel = ownerLevelId;
    this.user.PassengerType = ownerLevelId;
 }
}

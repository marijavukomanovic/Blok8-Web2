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
imageUri:string;
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
/*
  selectedFile: File = null;
  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }*/

  constructor(private fb : FormBuilder, private registracijaServis : RegistracijaServis, private router:Router) { }

  ngOnInit() {
    //this.user.PassengerType = PassengerType.Regularan;
  
  }

  onSubmit()
  {
    this.user = this.registerForm.value;
    //let formData: FormData = new FormData();

    //if (this.selectedFile != null) {
      //formData.append('Document', this.selectedFile, this.selectedFile.name);
    //}
    this.user.Document=this.imageUri;
    console.log(this.user);
    this.registracijaServis.register(this.registerForm.value).subscribe(data => {
      console.log('Registration successfully done.');
      this.router.navigate(['/']);
    });
    /*this.registracijaServis.register(this.user).subscribe(temp => {
      if(temp == "uspesno")
      {
        if (this.selectedFile != null) {
          this.registracijaServis.uploadImage(formData, this.user.UserName).subscribe(ret => {
            alert("Unseccesfull!!!");
            this.router.navigate(["/"]);
          },
            err => console.log(err));
        }
        else {
          alert("Succesfully registered!");
          this.router.navigate(["/login"]);
        }
      }
      else if(temp == "neuspesno")
      {
        console.log(temp);
        this.router.navigate(["/login"])
      }
    });*/

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

 onFileChanged(event) {
  if (event.target.files && event.target.files[0]) {

    const file = event.target.files[0];

    const reader = new FileReader();
    reader.onload = e => {this.user.Document = reader.result.toString().split(',')[1]; 
    console.log(this.user.Document);this.imageUri=this.user.Document };
  

    reader.readAsDataURL(file);
    console.log(file);
   // this.user.Document = file;
  }
}
 /*public imagePath;
 imgURL: any;
 public msg: string;

 preview(files) {
   if (files.length === 0)
     return;
*/
   //var mimeType = files[0].type;
   //if (mimeType.match(/image\/*/) == null) {
     /*this.msg = "Only images are supported.";
     return;
   }

   var reader = new FileReader();
   this.imagePath = files;
   reader.readAsDataURL(files[0]); 
   reader.onload = (_event) => { 
     console.log(reader);
     this.imgURL = reader.result; 
   }
}*/

}

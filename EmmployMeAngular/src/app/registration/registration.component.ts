import { Component, OnInit } from '@angular/core';
import { CrudService, ThSort } from 'ngx-cabernet';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  record: any = {};
  action: string = "Adding";
  constructor(public crudService: CrudService) {

  }
  ngOnInit(): void {
     
    this.record.RoleID="JOBAPPLICANT";
  }

  register(isValid: boolean): void {
    if (!isValid) {
      this.crudService.showMessage("Atención", "Por favor complete todos los campos !", "Aceptar");
    }
    else {
      this.record.UserID = this.crudService.newGuid();
      this.record.UserTypeID = 'EXTERNAL';
      this.record.LogonName = this.record.Email;
      this.record.ForceChangePassword = false;
      this.record.Password = "";
      this.record.Active = true;
      
      this.record.UserRole=[{UserID:this.record.UserID, RoleID: this.record.RoleID }];
      
      this.crudService.addRecord("User", this.record).subscribe(newRecord => {
        this.crudService.get("Api/login/SendInvitation", { userID: this.record.UserID }).subscribe(data => {
          if (data != undefined) {
            this.crudService.showMessage("Atención", "Felicitaciones ya estás registrado !", "Aceptar");
          }
        });
    

        
      });
    }
  }



}



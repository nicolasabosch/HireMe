import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { CrudService,DynamicLocaleService } from 'ngx-cabernet';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})



export class LoginComponent implements OnInit {
    
    record: any = {};
    action: string = "Adding";
    languageList: any[];
    form: any;
    constructor(public crudService: CrudService,
        private router: Router,
        private DynamicLocaleService: DynamicLocaleService) {
        this.form = {};
        
        this.form.user = "";
        this.form.password = "";
        
        
        
    }
    
    
    ngOnInit(): void {
        
        this.crudService.setPageTitle(this.crudService.translate("Ingreso"));
        this.crudService.getRecordList("Language", {}).subscribe(data => {
            this.languageList = data;
            this.form = {};
            this.form.languageID = this.crudService.currentLanguageID;
            this.form.user = "";
            this.form.password = "";
            
        })

    }

   mode:string="";

    signInClicked():void
    {
        this.mode="";

    }
    signUpClicked():void
    {
        this.mode="sign-up-mode";
    }

    languageChanged(languageID: string): void {
        
        this.crudService.changeLanguage(languageID,false);

    }
    

    validSubmit(): boolean {
        return (this.form.user && this.form.password);
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
          
          alert("anda el registro");
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

    login() {

        if (this.form.user && this.form.password) {
            this.crudService.authenticate({ username: this.form.user, password: this.form.password })
                .subscribe(
                    () => {

                        this.crudService.query("Api/Login", {}).subscribe(data => {
                            this.crudService.UserMenuList = data;
                            sessionStorage.removeItem("state");
                            var firstMenuItem = data.filter(function (e) { return e.IsPage === true })[0].RouteName;
                            this.router.navigateByUrl(firstMenuItem);
                        })


                    }
                );

                
        }
    }
}
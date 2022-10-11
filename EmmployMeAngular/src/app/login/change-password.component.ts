import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { CrudService } from 'ngx-cabernet';

@Component({
  selector: 'app-changepassword',
  templateUrl: './change-password.component.html',
  styleUrls: ['./login.component.css'],
})

export class ChangePasswordComponent {
    form:UntypedFormGroup;

    constructor(private fb:UntypedFormBuilder, 
                 private crudService: CrudService, 
                 private router: Router) {

        this.form = this.fb.group({
            CurrentPassword: ['',Validators.required],
            NewPassword: ['',[Validators.required,Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,30})')]],
            ConfirmedPassword: ['',[Validators.required,Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,30})')]]
        }
        );
    }

   

    validSubmit(): boolean {
        return (this.form.value.CurrentPassword && this.form.value.NewPassword &&  this.form.value.ConfirmedPassword);
    }

    

    changePassword() {
        const val = this.form.value;

        if (val.CurrentPassword && val.NewPassword && val.ConfirmedPassword) {
            this.crudService.changePassword({ CurrentPassword: val.CurrentPassword, NewPassword: val.NewPassword, ConfirmedPassword: val.ConfirmedPassword })
                .subscribe(
                    (data) => {
                        if(data===undefined)    
                        {
                            return;
                        }

                        this.crudService.query("Api/Login", {}).subscribe(data => {
                            this.crudService.UserMenuList = data;
                            sessionStorage.setItem("state", null);
                            
                           this.router.navigateByUrl(data[0].RouteName);
                          })		

                        
                    }
                );
        }
    }
}
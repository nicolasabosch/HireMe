import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CrudService } from 'ngx-cabernet';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-generatepassword',
  templateUrl: './generate-password.component.html',
  styleUrls: ['./login.component.css'],
})

export class GeneratePasswordComponent {
    form:FormGroup;

    resetPasswordID:string;

    constructor(private fb:FormBuilder, 
                 public crudService: CrudService, 
                 private router: Router,
                 private route: ActivatedRoute) {

        this.form = this.fb.group({
            ResetPasswordID: ['',Validators.required],
            NewPassword: ['',[Validators.required,Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,30})')]],
            ConfirmedPassword: ['',[Validators.required,Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,30})')]]
        });
        
        this.resetPasswordID =  this.route.snapshot.params['id'];
        
    }

    validSubmit(): boolean {
        return (this.form.value.NewPassword &&  this.form.value.ConfirmedPassword);
    }

    status:string="PENDING";
    generatePassword() {
        const val = this.form.value;

        if (this.resetPasswordID && val.NewPassword && val.ConfirmedPassword) {
            this.crudService.generatePassword({ ResetPasswordID: this.resetPasswordID, NewPassword: val.NewPassword, ConfirmedPassword: val.ConfirmedPassword })
                .subscribe(
                    (data) => {
                        
                            
                            if(data !=undefined && data.Message==="OK")
                            {
                                this.status ="OK"; 
                            }
                            else
                            {
                                this.status=data.Message;
                            }
                        

                        
                    }
                );
        }
    }
}
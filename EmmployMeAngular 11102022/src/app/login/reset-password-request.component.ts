import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CrudService } from 'ngx-cabernet';

@Component({
  selector: 'app-reset-password-request',
  templateUrl: './reset-password-request.component.html',
  styleUrls: ['./login.component.css'],
})

export class ResetPasswordRequestComponent {
    form:FormGroup;

    status:string="PENDING";



    constructor(private fb:FormBuilder, 
                 public crudService: CrudService, 
                 private router: Router,
        ) {

        this.form = this.fb.group({
            email: ['',Validators.required],
 
            
        });

        
    }

    validSubmit(): boolean {
        return (this.form.value.email);
    }

    resetPasswordRequest() {
        const val = this.form.value;

        if (val.email ) {
            this.crudService.resetPasswordRequest({ param: val.email})
                .subscribe(
                    data => {
                        if(data !=undefined && data.Message==="OK")
                        {
                            this.status ="OK"; 
                        }
                        else
                        {
                            this.status="ERROR";
                        }
                    }
                );
        }
    }
}
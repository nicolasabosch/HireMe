import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { CrudService,DynamicLocaleService } from 'ngx-cabernet';
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {

    languageList: any[];
    form: any;
    constructor(public crudService: CrudService,
        private router: Router,
        private DynamicLocaleService: DynamicLocaleService) {
        this.form = {};
        this.form.languageID = "";
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

    languageChanged(languageID: string): void {
        
        this.crudService.changeLanguage(languageID,false);

    }

    validSubmit(): boolean {
        return (this.form.user && this.form.password);
    }

    login() {


        if (this.form.user && this.form.password) {
            this.crudService.authenticate({ username: this.form.user, password: this.form.password })
                .subscribe(
                    () => {

                        this.crudService.query("Api/Login", {}).subscribe(data => {
                            this.crudService.UserMenuList = data;
                            //sessionStorage.setItem("state", null);
                            sessionStorage.removeItem("state");
                            var firstMenuItem = data.filter(function (e) { return e.IsPage === true })[0].RouteName;
                            this.router.navigateByUrl(firstMenuItem);
                        })


                    }
                );
        }
    }
}
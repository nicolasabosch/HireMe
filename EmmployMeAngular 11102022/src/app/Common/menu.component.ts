import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { CrudService} from 'ngx-cabernet';

import { Router } from '@angular/router';
@Component({
  styleUrls: ['./menu.component.css'],
  selector: 'app-menu2',
  templateUrl: './menu.component.html'
})
export class MenuComponent2 implements OnInit {
  UserMenuList: any[];
  LanguageID: string;


  constructor(
    private ref: ChangeDetectorRef,
    public crudService: CrudService,
    private router: Router,


  ) {

  }
  languageList: any[];
  ngOnInit(): void {


    this.crudService.getRecordList("Language", {}).subscribe(data => {
      //var defaultLanguage = this.crudService.defaultLanguage;
      this.languageList = data;
      this.LanguageID = this.crudService.currentLanguageID;
    })

  }
  hideMenu() {
    setTimeout(
      function () {
        var elem = document.getElementsByClassName("navbar-collapse")[0];
        elem.classList.remove("show");
      }, 100);

  }

  changeLanguage(languageID: string): void {

    this.crudService.changeLanguage(this.LanguageID, true);

    this.crudService.getRecordList("Language", {}).subscribe(data => {

      this.languageList = data;

      this.LanguageID = languageID;

    })
  }

  logout() {
    this.crudService.logout();
    this.router.navigateByUrl('Login/' + this.crudService.currentLanguageID);
  }
}

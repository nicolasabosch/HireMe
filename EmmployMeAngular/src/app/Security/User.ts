import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'User',
  template: ''
})
export abstract class UserComponent extends CrudFormComponent implements OnInit {
  RoleList: any[] | undefined;
  UserTypeList: any[];

  ngOnInit(): void {
    this.entityName = "User";
    this.referenceData.push(
      { url: 'Api/Role', listName: 'RoleList' },
      { url: 'Api/UserType', listName: 'UserTypeList' },

    );

    this.setCheckList("UserRole", "RoleList", "RoleID");

    this.onAddRecord.subscribe(record => {
      record.UserTypeID ="INTERNAL";
    })


  }
  // sendInvitation(userID: string, ev: Event): void {
  //   ev.stopPropagation();
  //   this.crudService.get("Api/login/SendInvitation", { userID: userID }).subscribe(data => {
  //     if (data != undefined) {
  //       this.crudService.showMessage("Atención", "Se ha envíado el mail al usuario", "OK");
  //     }
  //   });


  }



@Component({
  selector: 'User-list',
  templateUrl: './User-list.html',
})
export class UserListComponent extends UserComponent { }

@Component({
  selector: 'User-crud',
  templateUrl: './User-crud.html',
})
export class UserCrudComponent extends UserComponent { }

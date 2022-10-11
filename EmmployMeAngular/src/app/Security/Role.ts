import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'Role',
  template: ''
})
export abstract class RoleComponent extends CrudFormComponent implements OnInit {
  
MenuItemList: any[];

  ngOnInit(): void {
    this.entityName ="Role";
    this.referenceData.push(
   
{url: 'Api/MenuItem', listName: 'MenuItemList' },

    );
    this.setCheckList("RoleMenuItem", "MenuItemList", "MenuItemID");

    this.searchRecordList();
  }
  

}

@Component({
  selector: 'Role-list',
  templateUrl: './Role-list.html',
})
export class RoleListComponent extends RoleComponent {}

@Component({
  selector: 'Role-crud',
  templateUrl: './Role-crud.html',
})
export class RoleCrudComponent extends RoleComponent {}

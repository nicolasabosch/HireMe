import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent, CrudService } from 'ngx-cabernet';

@Component({
  selector: 'UserType',
  template: ''
})
export abstract class UserTypeComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="UserType";
    this.identityKey =false;
    this.fillRecordListOnInit = true;
  var unGUid = this.crudService.newGuid();



    this.referenceData.push(

      

    );
  }
}

@Component({
  selector: 'UserType-list',
  templateUrl: './UserType-list.html',
})
export class UserTypeListComponent extends UserTypeComponent {}

@Component({
  selector: 'UserType-crud',
  templateUrl: './UserType-crud.html',
})
export class UserTypeCrudComponent extends UserTypeComponent {}
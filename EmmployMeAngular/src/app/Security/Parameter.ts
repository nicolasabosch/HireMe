import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'Parameter',
  template: ''
})
export abstract class ParameterComponent extends CrudFormComponent implements OnInit {
  UserTypeList: any[];

  ngOnInit(): void {
    this.entityName ="Parameter";
    
    this.searchRecordList();
  }
  

}

@Component({
  selector: 'Parameter-list',
  templateUrl: './Parameter-list.html',
})
export class ParameterListComponent extends ParameterComponent {}

@Component({
  selector: 'Parameter-crud',
  templateUrl: './Parameter-crud.html',
})
export class ParameterCrudComponent extends ParameterComponent {}

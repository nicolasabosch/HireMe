import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'CompanyType',
  template: ''
})
export abstract class CompanyTypeComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="CompanyType";
    this.identityKey =false;
    this.fillRecordListOnInit = true;
    
    this.referenceData.push(
      	
    );
  }
}

@Component({
  selector: 'CompanyType-list',
  templateUrl: './CompanyType-list.html',
})
export class CompanyTypeListComponent extends CompanyTypeComponent {}

@Component({
  selector: 'CompanyType-crud',
  templateUrl: './CompanyType-crud.html',
})
export class CompanyTypeCrudComponent extends CompanyTypeComponent {}
import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'Company',
  template: ''
})
export abstract class CompanyComponent extends CrudFormComponent implements OnInit {
  CompanyTypeList: any[];

  ngOnInit(): void {
    this.entityName ="Company";
    this.identityKey =true;
    this.fillRecordListOnInit = false;
    
    this.referenceData.push(
      	{url: 'Api/CompanyType', listName: 'CompanyTypeList' },

    );
  }
}

@Component({
  selector: 'Company-list',
  templateUrl: './Company-list.html',
})
export class CompanyListComponent extends CompanyComponent {}

@Component({
  selector: 'Company-crud',
  templateUrl: './Company-crud.html',
})
export class CompanyCrudComponent extends CompanyComponent {}
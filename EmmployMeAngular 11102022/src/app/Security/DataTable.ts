import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'DataTable',
  template: ''
})
export abstract class DataTableComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="DataTable";
    this.referenceData.push(
      	
    );
    
    this.searchRecordList();
  }
  

}

@Component({
  selector: 'DataTable-list',
  templateUrl: './DataTable-list.html',
})
export class DataTableListComponent extends DataTableComponent {}

@Component({
  selector: 'DataTable-crud',
  templateUrl: './DataTable-crud.html',
})
export class DataTableCrudComponent extends DataTableComponent {}

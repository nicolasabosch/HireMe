import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'MenuBar',
  template: ''
})
export abstract class MenuBarComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="MenuBar";
    this.fillRecordListOnInit = true;
    this.referenceData.push(
      	
    );
    
    
  }
  

}

@Component({
  selector: 'MenuBar-list',
  templateUrl: './MenuBar-list.html',
})
export class MenuBarListComponent extends MenuBarComponent {}

@Component({
  selector: 'MenuBar-crud',
  templateUrl: './MenuBar-crud.html',
})
export class MenuBarCrudComponent extends MenuBarComponent {}

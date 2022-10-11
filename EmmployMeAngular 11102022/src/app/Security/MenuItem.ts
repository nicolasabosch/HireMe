import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'MenuItem',
  template: ''
})
export abstract class MenuItemComponent extends CrudFormComponent implements OnInit {
  MenuBarList?: any[];
  

  ngOnInit(): void {
    this.entityName ="MenuItem";
    this.referenceData.push(
      	{url: 'Api/MenuBar', listName: 'MenuBarList' },
        

    );
    
    this.searchRecordList();
  }
  

}

@Component({
  selector: 'MenuItem-list',
  templateUrl: './MenuItem-list.html',
})
export class MenuItemListComponent extends MenuItemComponent {}

@Component({
  selector: 'MenuItem-crud',
  templateUrl: './MenuItem-crud.html',
})
export class MenuItemCrudComponent extends MenuItemComponent {}

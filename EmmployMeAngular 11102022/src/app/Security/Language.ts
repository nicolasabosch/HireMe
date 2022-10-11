import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'Language',
  template: ''
})
export abstract class LanguageComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="Language";
    this.referenceData.push(
      	
    );
    
    this.searchRecordList();
  }
  

}

@Component({
  selector: 'Language-list',
  templateUrl: './Language-list.html',
})
export class LanguageListComponent extends LanguageComponent {}

@Component({
  selector: 'Language-crud',
  templateUrl: './Language-crud.html',
})
export class LanguageCrudComponent extends LanguageComponent {}

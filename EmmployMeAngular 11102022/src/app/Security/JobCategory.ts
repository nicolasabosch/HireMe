import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'JobCategory',
  template: ''
})
export abstract class JobCategoryComponent extends CrudFormComponent implements OnInit {
  
  ngOnInit(): void {
    this.entityName ="JobCategory";
    this.identityKey =false;
    this.fillRecordListOnInit = true;
    this.setDetailList("JobCategorySkill","JobCategorySkillID",{'JobCategorySkillID':0});

    this.referenceData.push(
      	
    );
  }
}

@Component({
  selector: 'JobCategory-list',
  templateUrl: './JobCategory-list.html',
})
export class JobCategoryListComponent extends JobCategoryComponent {}

@Component({
  selector: 'JobCategory-crud',
  templateUrl: './JobCategory-crud.html',
})
export class JobCategoryCrudComponent extends JobCategoryComponent {}
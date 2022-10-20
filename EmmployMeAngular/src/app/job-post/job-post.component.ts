import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'job-post',
  template: ''
})
export abstract class JobPostComponent extends CrudFormComponent implements OnInit {
  CompanyTypeList: any[];
//Un comentarios 
  ngOnInit(): void {
    this.entityName ="JobPost";
    this.identityKey =true;
    this.fillRecordListOnInit = false;
    
    this.referenceData.push(
      	{url: 'Api/JobPost', listName: 'job-post-list' },

    );
  }
}

@Component({
  selector: 'job-post-list',
  templateUrl: 'job-post.component-list.html',
})
export class JobPostListComponent extends JobPostComponent {}

@Component({
  selector: 'job-post-crud',
  templateUrl: 'job-post.component-crud.html',
})
export class JobPostCrudComponent extends JobPostComponent {}
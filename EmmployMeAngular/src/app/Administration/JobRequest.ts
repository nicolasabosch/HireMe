import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'JobRequest',
  template: ''
})
export abstract class JobRequestComponent extends CrudFormComponent implements OnInit {
  JobCategoryList: any[];
ToolList: any[];

  ngOnInit(): void {
    this.entityName ="JobRequest";
    this.identityKey =true;
    this.fillRecordListOnInit = false;
    this.setDetailList("JobRequestFile","JobRequestFileID");
this.setCheckList("JobRequestTool", "ToolList", "ToolID");
this.setTagList("JobRequestUser");

    this.referenceData.push(
      	{url: 'Api/JobCategory', listName: 'JobCategoryList' },
{url: 'Api/Tool', listName: 'ToolList' },

    );
  }
}

@Component({
  selector: 'JobRequest-list',
  templateUrl: './JobRequest-list.html',
})
export class JobRequestListComponent extends JobRequestComponent {}

@Component({
  selector: 'JobRequest-crud',
  templateUrl: './JobRequest-crud.html',
})
export class JobRequestCrudComponent extends JobRequestComponent {}
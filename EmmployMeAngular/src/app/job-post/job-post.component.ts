import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'job-post',
  template: ''
})
export abstract class JobPostComponent extends CrudFormComponent implements OnInit ,AfterViewInit {
  ngAfterViewInit(): void {
    this.title="Publicar Ofertas";  
  }
  
  JobCategoryList: any[];
  JobPostStatusList: any[];
  JobApplicanceStatusList: any[];
  JobCategorySkillList: any[];

  ngOnInit(): void {
    this.entityName ="JobPost";
    this.identityKey =true;
    this.fillRecordListOnInit = true;

    
    this.setCheckList("JobPostSkill", "JobCategorySkillList", "JobCategorySkillID");

    this.referenceData.push(
      {url: 'Api/JobCategory', listName: 'JobCategoryList' },
      {url: 'Api/JobPostStatus', listName: 'JobPostStatusList' },
      {url: 'Api/JobApplicanceStatus', listName: 'JobApplicanceStatusList' },
      {url: 'Api/JobCategorySkill', listName: 'JobCategorySkillList' },

    );
    

    this.onAddRecord.subscribe(record => {
      record.JobPostStatusID ="OPENED";
    })

  }

  UpdateJobAppicanceStatus(r):void  
  {
    this.crudService.addRecord("JobPost/UpdateJobAppicanceStatus",  r ).subscribe(newRecord => {
      this.crudService.showMessageAutoClose("Atención", "Estado modificado correctamente", "OK");

  });
  

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
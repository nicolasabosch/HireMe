import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'job-post',
  template: ''
})
export abstract class JobPostComponent extends CrudFormComponent implements OnInit {
  
  JobCategoryList: any[];
  JobCategorySkillList: any[];

  ngOnInit(): void {
    this.entityName ="JobPost";
    this.identityKey =true;
    this.fillRecordListOnInit = true;

    this.setCheckList("JobPostSkill", "JobCategorySkillList", "JobCategorySkillID");

    this.referenceData.push(
      {url: 'Api/JobCategory', listName: 'JobCategoryList' },
      {url: 'Api/JobCategorySkill', listName: 'JobCategorySkillList' },

    );
    this.title="Publicar Ofertas";
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
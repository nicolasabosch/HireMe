import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'job-opportunity',
  template: ''
})
export abstract class JobOpportunityComponent extends CrudFormComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.title = "Busca tu trabajo ideal";
  }

  JobCategoryList: any[];
  JobPostStatusList: any[];
  JobCategorySkillList: any[];
  JobApplicanceStatusList: any[];
  applicance: any = {};

  ngOnInit(): void {
    this.endPointSearchList = "JobPost/OpenedJobPost"
    this.entityName = "JobPost";
    this.identityKey = true;
    this.fillRecordListOnInit = false;
    this.addeable = false;
    this.deleteable = false;
    this.editable = false;


    this.setCheckList("JobPostSkill", "JobCategorySkillList", "JobCategorySkillID");

    this.referenceData.push(
      { url: 'Api/JobCategory', listName: 'JobCategoryList' },
      { url: 'Api/JobPostStatus', listName: 'JobPostStatusList' },
      { url: 'Api/JobApplicanceStatus', listName: 'JobApplicanceStatusList' },
      { url: 'Api/JobCategorySkill', listName: 'JobCategorySkillList' },

    );


    this.onGetRecord.subscribe(record => {
      if (record.applicance)
        this.applicance = record.applicance;
      else
        record.applicance = {};
    })



  }


  Apply(): void {
    this.applicance.JobPostID = this.record.JobPostID;
    this.crudService.addRecord("JobPost/ApplyJobPost", this.applicance).subscribe(newRecord => {

      this.crudService.getRecordList(this.endPointSearchList, { key: this.record[this.entityName + 'ID'] }).subscribe(recordList => {
        this.recordList[this.selectedIndex] = recordList[0];
      });

      this.crudService.showMessageAutoClose("Atención", "Su Aplicación ha sido grabada", "OK");
    })
  }


}

@Component({
  selector: 'job-opportunity-list',
  templateUrl: 'job-opportunity.component-list.html',
})
export class JobOpportunityListComponent extends JobOpportunityComponent { }

@Component({
  selector: 'job-opportunity-crud',
  templateUrl: 'job-opportunity.component-crud.html',
})
export class JobOpportunityCrudComponent extends JobOpportunityComponent { }
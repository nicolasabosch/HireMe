import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'my-opportunity',
  template: ''
})
export abstract class MyOpportunityComponent extends CrudFormComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.title = "Ver mis aplicaciones";
  }

  JobCategoryList: any[];
  JobPostStatusList: any[];
  JobCategorySkillList: any[];
  JobApplicanceStatusList: any[];
  applicance: any = {};
  

  ngOnInit(): void {
    this.endPointSearchList = "JobPost/MyOpportunity"
    this.entityName = "JobPost";
    this.identityKey = true;
    this.fillRecordListOnInit = true;
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
  selector: 'my-opportunity-list',
  templateUrl: 'my-opportunity.component-list.html',
})
export class MyOpportunityListComponent extends MyOpportunityComponent { }

@Component({
  selector: 'my-opportunity-crud',
  templateUrl: 'my-opportunity.component-crud.html',
})
export class MyOpportunityCrudComponent extends MyOpportunityComponent { }
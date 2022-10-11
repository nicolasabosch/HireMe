﻿import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'DataTranslation',
  templateUrl: './DataTranslation.html',
})
export class DataTranslationComponent extends CrudFormComponent implements OnInit {
  languageList: any[];
  dataTableList: any[];

  notTranslated:boolean=false;
  ngOnInit(): void {

    this.crudService.getRecordList("Language", {}).subscribe(data => {
      var defaultLanguageID = this.crudService.defaultLanguageID;
      var currentLanguageID = this.crudService.currentLanguageID;
      //this.languageList = data.filter(function (e) { return e.LanguageID !== defaultLanguageID });
      this.languageList = data.filter(function (e) { return e.LanguageID !== defaultLanguageID && e.LanguageID !==currentLanguageID });

    })
    this.crudService.getRecordList("DataTable", {}).subscribe(data => {
      this.dataTableList = data;
    });

  }

  getList() {
    if (this.search.DataTableID !== undefined && this.search.LanguageID !== undefined) {
      this.crudService.getRecordList("DataTranslation/GetDataTranslation", { "dataTableID": this.search.DataTableID, "languageID": this.search.LanguageID }).subscribe(data => {
        this.recordList = data;
      })
    }
  }
  translationChanged(row: any) {
    row.LanguageID = this.search.LanguageID;
    row.FieldName = this.search.DataTableID + "Name";
    if (row.EntityStatus === "A") {
      if (row.Translation) {
        this.crudService.addRecord("DataTranslation", row).subscribe(newRecord => {
          row.EntityStatus = "U";
        })
      };
    }
    else {
      this.crudService.finishRecord("DataTranslation", row).subscribe(updatedRecord => { });
    }
  }
}


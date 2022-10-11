import { Component, OnInit, Input } from '@angular/core';
import { CrudFormComponent } from 'ngx-cabernet';

@Component({
  selector: 'TextTranslation',
  templateUrl: './TextTranslation.html',
})
export class TextTranslationComponent extends CrudFormComponent implements OnInit {
  languageList: any[];

  notTranslated:boolean=false;

  ngOnInit(): void {

    this.crudService.getRecordList("Language", {}).subscribe(data => {
      var defaultLanguageID = this.crudService.defaultLanguageID;
      var currentLanguageID = this.crudService.currentLanguageID;
      this.languageList = data.filter(function (e) { return e.LanguageID !== defaultLanguageID && e.LanguageID !==currentLanguageID });

    })

  }

  languageChanged() {

    this.crudService.getRecordList("TextTranslation/" + this.search.LanguageID, { }).subscribe(data => {
      this.recordList = data;
    })
  }
  translationChanged(row: any) {
    row.LanguageID = this.search.LanguageID;
    if (row.EntityStatus === "A") {
      if (row.Translation) {
        this.crudService.addRecord("TextTranslation", row).subscribe(newRecord => {
          row.EntityStatus = "U";
          row.OriginalTranslation = row.Translation; 
          row.LastModifiedOn = newRecord.LastModifiedOn;
        })
      };
    }
    else {
      if (row.OriginalTranslation !== row.Translation)
      {
          this.crudService.finishRecord("TextTranslation", row).subscribe(updatedRecord => { 
            row.LastModifiedOn = updatedRecord.LastModifiedOn;

          });
      }
      
    }
  }
}


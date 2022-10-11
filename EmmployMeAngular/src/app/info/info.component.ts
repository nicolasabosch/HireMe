import { Component, OnInit } from '@angular/core';
import { CrudService } from 'ngx-cabernet';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

record:any;

  constructor(public crudService: CrudService)
  {
    
  }

  toolList:any[];

  ngOnInit(): void {
    this.crudService.showMessage("Titgutlo","Cuerpo del mensaje","OKKK");
    
    this.crudService.getRecordList("Tool", {"ToolName":"I"}).subscribe(data => {
      this.toolList = data;
      this.record ={};
      this.record.DataTranslation= [];
  })

  }
  save():void
  {
      this.record.ToolID = this.crudService.newGuid();
      this.crudService.addRecord("Tool", this.record).subscribe(x=>{
        this.crudService.getRecordList("Tool", {"ToolName":"I"}).subscribe(data => {
          this.toolList = data;
        });

      });
  }
  

}

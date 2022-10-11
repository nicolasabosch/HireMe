import { Component, OnInit } from '@angular/core';
import { CrudService } from 'ngx-cabernet';

@Component({
  selector: 'app-nico01',
  templateUrl: './nico01.component.html',
  styleUrls: ['./nico01.component.css']
})
export class Nico01Component implements OnInit {

  record: any = {};
  nombre: string;
  languageList: any[];
  language: [];
  constructor(public crudService: CrudService) {

  }

  ngOnInit(): void {
    this.nombre = "Nico";

    this.crudService.query("Api/Language", {}).subscribe(data => {
      this.languageList = data;

    });

  }

  createLanguage():void
  {
     this.crudService.addRecord("Language",this.record).subscribe(x=>
      {
        this.crudService.query("Api/Language", {}).subscribe(data => {
          this.languageList = data;
    
        });
      })
  }

}

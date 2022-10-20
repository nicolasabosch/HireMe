import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { slideInAnimation } from '../route-animation';
import { CrudService } from 'ngx-cabernet';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public crudService: CrudService) { 

    this.crudService.baseUrl=environment.webAPIUrl;    
    this.crudService.translatable = environment.translatable;
    
    
  }
  search: any = {};
  record: any = {};
  action: string = "Adding";
  languageList: any[];
  form: any;
  recordList: any[] = [];
  searchText: string = null;
    
    
    ngOnInit(): void {
        
        this.crudService.setPageTitle(this.crudService.translate("Ingreso"));
        this.crudService.getRecordList("Company", {}).subscribe(data => {
            
          this.recordList=data;
            
        })

      }
      }
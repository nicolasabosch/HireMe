import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { slideInAnimation } from './route-animation';
import { CrudService } from 'ngx-cabernet';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [ slideInAnimation ]
})
export class AppComponent {
  
  constructor (public crudService: CrudService) {
      this.crudService.baseUrl=environment.webAPIUrl;    
      this.crudService.translatable = environment.translatable;
      
      if (sessionStorage.getItem("SelectedLanguageID")) {
        this.crudService.currentLanguageID = <string>sessionStorage.getItem("SelectedLanguageID");
  
      }
      else {
        sessionStorage.setItem("SelectedLanguageID", this.crudService.defaultLanguageID);
        this.crudService.currentLanguageID = this.crudService.defaultLanguageID;
      }
      
      this.crudService.changeLanguage(this.crudService.currentLanguageID, false);
    }

  public isLogedIn() {
    return this.crudService.isLoggedIn();
  }
  
  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }
}
  
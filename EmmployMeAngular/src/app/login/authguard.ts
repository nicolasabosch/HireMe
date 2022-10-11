import { Injectable, Inject } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { CrudService } from 'ngx-cabernet';

@Injectable()
export class AuthGuard implements CanActivate {
    
    constructor(
        private crudService: CrudService, private router: Router
    ) {}

    canActivate() {
        
        if (this.crudService.isLoggedIn()) {
            return true;
        } else {
            this.router.navigate(['Login']);
            return false;
        }
    }
}
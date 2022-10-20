import { UserTypeListComponent, UserTypeCrudComponent } from './Security/UserType';
import { ToolListComponent, ToolCrudComponent } from './Administration/Tool';
import { JobRequestListComponent, JobRequestCrudComponent } from './Administration/JobRequest';
import { CompanyListComponent, CompanyCrudComponent } from './Administration/Company';
import { CompanyTypeListComponent, CompanyTypeCrudComponent } from './Administration/CompanyType';

import { JobCategoryListComponent, JobCategoryCrudComponent } from './Security/JobCategory';
import { DataTableListComponent, DataTableCrudComponent } from './Security/DataTable';
import { DataTranslationComponent } from './Security/DataTranslation';
import { LanguageListComponent, LanguageCrudComponent } from './Security/Language';
import { MenuBarListComponent, MenuBarCrudComponent } from './Security/MenuBar';
import { MenuItemListComponent, MenuItemCrudComponent } from './Security/MenuItem';
import { ParameterListComponent, ParameterCrudComponent } from './Security/Parameter';
import { RoleListComponent, RoleCrudComponent } from './Security/Role';
import { TextTranslationComponent } from './Security/TextTranslation';
import { UserListComponent, UserCrudComponent } from './Security/User';

import { NgxCabernetModule } from 'ngx-cabernet';
import { CustomDateParserFormatter, DatePickerAdapter } from 'ngx-cabernet';
import { DynamicLocaleService } from 'ngx-cabernet';
import { LocaleService } from 'ngx-cabernet';
import { LoaderInterceptor } from 'ngx-cabernet';
import { MenuComponent2 } from './Common/menu.component';
import { AppComponent } from './app.component';

import { LoginComponent } from './login/login.component';
import { AuthGuard } from './login/authguard';
import { ChangePasswordComponent } from './login/change-password.component';
import { GeneratePasswordComponent } from './login/generate-password.component';
import { ResetPasswordRequestComponent } from './login/reset-password-request.component';


import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { NgbModule, NgbActiveModal, NgbTooltip } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';



import { registerLocaleData, DatePipe, CurrencyPipe, DecimalPipe, PercentPipe } from '@angular/common';

import localeES from '@angular/common/locales/es';
import localeEN from '@angular/common/locales/en';
import localePT from '@angular/common/locales/pt';;
registerLocaleData(localeES, 'es');
registerLocaleData(localePT, 'pt');
registerLocaleData(localeEN, 'en');

import { InfoComponent } from './info/info.component';
import { RegistrationComponent } from './registration/registration.component'
;
import { Nico01Component } from './login/nico01/nico01.component'
import { HomeComponent } from './home/home.component';
import { JobPostComponent } from './job-post/job-post.component';









const appRoutes: Routes = [
{ path: 'UserType', component: UserTypeListComponent, data: { animation: 'UserType' }, canActivate: [AuthGuard] },
{ path: 'UserType/:id', component: UserTypeCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },

{ path: 'Tool', component: ToolListComponent, data: { animation: 'Tool' }, canActivate: [AuthGuard] },
{ path: 'Tool/:id', component: ToolCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },

{ path: 'JobRequest', component: JobRequestListComponent, data: { animation: 'JobRequest' }, canActivate: [AuthGuard] },
{ path: 'JobRequest/:id', component: JobRequestCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },

{ path: 'Company', component: CompanyListComponent, data: { animation: 'Company' }, canActivate: [AuthGuard] },
{ path: 'Company/:id', component: CompanyCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },

{ path: 'CompanyType', component: CompanyTypeListComponent, data: { animation: 'CompanyType' }, canActivate: [AuthGuard] },
{ path: 'CompanyType/:id', component: CompanyTypeCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },


{ path: 'JobCategory', component: JobCategoryListComponent, data: { animation: 'JobCategory' }, canActivate: [AuthGuard] },
{ path: 'JobCategory/:id', component: JobCategoryCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },

    
    { path: 'TextTranslation', component: TextTranslationComponent, data: { animation: 'TextTranslation' }, canActivate: [AuthGuard] },
    { path: 'DataTranslation', component: DataTranslationComponent, data: { animation: 'DataTranslation' }, canActivate: [AuthGuard] },
  
  
    { path: 'Language', component: LanguageListComponent, data: { animation: 'Language' }, canActivate: [AuthGuard] },
    { path: 'Language/:id', component: LanguageCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
  
    { path: 'DataTable', component: DataTableListComponent, data: { animation: 'DataTable' }, canActivate: [AuthGuard] },
    { path: 'DataTable/:id', component: DataTableCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
    { path: 'Parameter', component: ParameterListComponent, data: { animation: 'ParameterListComponent' }, canActivate: [AuthGuard] },
    { path: 'Parameter/:id', component: ParameterCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
  
  
    { path: 'MenuBar', component: MenuBarListComponent, data: { animation: 'MenuBarListComponent' }, canActivate: [AuthGuard] },
    { path: 'MenuBar/:id', component: MenuBarCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
  
    { path: 'MenuItem', component: MenuItemListComponent, data: { animation: 'MenuItemListComponent' }, canActivate: [AuthGuard] },
    { path: 'MenuItem/:id', component: MenuItemCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
  
    { path: 'Role', component: RoleListComponent, data: { animation: 'm' }, canActivate: [AuthGuard] },
    { path: 'Role/:id', component: RoleCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
    { path: 'JobPost', component: JobPostComponent, data: { animation: 'User' }, canActivate: [AuthGuard] },
   
    { path: 'User', component: UserListComponent, data: { animation: 'User' }, canActivate: [AuthGuard] },
    { path: 'User/:id', component: UserCrudComponent, data: { animation: 'CRUD' }, canActivate: [AuthGuard] },
  
    { path: 'ChangePassword', component: ChangePasswordComponent, data: { animation: 'User' }, canActivate: [AuthGuard] },
    { path: 'ResetPasswordRequest', component: ResetPasswordRequestComponent, data: { animation: 'User' } },
    { path: 'GeneratePassword/:id', component: GeneratePasswordComponent, data: { animation: 'User' } },
  
    { path: 'Login', component: LoginComponent, data: { animation: 'Login' } },
    { path: 'Login/:id', component: LoginComponent, data: { animation: 'Login' } },
  
    { path: 'XTool', component: InfoComponent, data: { animation: 'Login' } },
    { path: 'Registration', component: RegistrationComponent, data: {} },

    { path: '', redirectTo: 'Login', pathMatch: 'full' },
  
   {path: 'home', component: HomeComponent, data: { animation: 'home' } },
  
  
  
  
    { path: '**', redirectTo: 'd' }
  ];
  


@NgModule({
  declarations: [
UserTypeCrudComponent,
UserTypeListComponent,
ToolCrudComponent,
ToolListComponent,
JobRequestCrudComponent,
JobRequestListComponent,
CompanyCrudComponent,
CompanyListComponent,
CompanyTypeCrudComponent,
CompanyTypeListComponent,

JobCategoryCrudComponent,
JobCategoryListComponent,
    
    TextTranslationComponent,
    DataTranslationComponent,
    LanguageCrudComponent,
    LanguageListComponent,
    DataTableCrudComponent,
    DataTableListComponent,
    MenuComponent2,

    UserCrudComponent,
    UserListComponent,
    ParameterCrudComponent,
    ParameterListComponent,

    AppComponent,
    JobPostComponent,

    MenuBarCrudComponent,
    MenuBarListComponent,
    MenuItemCrudComponent,
    MenuItemListComponent,
    RoleListComponent,
    RoleCrudComponent,
    HomeComponent,
    LoginComponent,
    ChangePasswordComponent,
    GeneratePasswordComponent,
    ResetPasswordRequestComponent,
    
    InfoComponent
,
          RegistrationComponent
,
          Nico01Component,
          JobPostComponent


  ],

  imports: [

    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false, relativeLinkResolution: 'legacy' }
    ),
    HttpClientModule,
    BrowserModule,
    
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,

    RouterModule.forRoot(appRoutes),

    NgbModule,
    NgxCabernetModule,


  ],
  providers: [
    AuthGuard,
    NgbModule,
    NgbActiveModal,
    NgbTooltip,
    LocaleService,
    DynamicLocaleService,

    DatePipe,
    CurrencyPipe,
    DecimalPipe,
    PercentPipe,

    { provide: NgbDateAdapter, useClass: DatePickerAdapter },
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter },

    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },

    {
      provide: LOCALE_ID,
      deps: [LocaleService],
      useFactory: (LocaleService: { locale: string; }) => LocaleService.locale
    }

  ],
  bootstrap: [AppComponent]
})
export class AppModule {


}



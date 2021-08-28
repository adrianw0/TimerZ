import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { DatePipe } from '@angular/common'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';


import { LabelsService } from './Services/Labels.service';
import { LabelsDialogComponent } from './Labels/LabelsDialog/LabelsDialog.component';
import { ProjectDialogComponent } from './Projects/ProjectDialog/ProjectDialog.component';
import { ProjectsService } from './Services/Projects.service';
import { TimerService } from './Services/TimerService.service';



import { AppComponent } from './app.component';
// import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';


import { TimersListComponent } from "./Timers-List/Timers-List.component";
import { ProjectsComponent } from "./Projects/Projects.component";
import { StopwatchBarComponent } from './Stopwatch-Bar/Stopwatch-Bar.component';
import { LabelsComponent } from "./Labels/Labels.component";


import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker'
import { MatDialogModule } from '@angular/material/dialog'
import { MatButtonModule } from '@angular/material/button';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table'
import { MatGridListModule } from '@angular/material/grid-list';
import {MatInputModule} from '@angular/material/input'

import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatListModule } from '@angular/material/list';



const routes: Routes = [
  { path: 'Timers', component: TimersListComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
  { path: 'Projects', component: ProjectsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard]},
  { path: 'Labels', component: LabelsComponent, pathMatch: 'full', canActivate: [AuthorizeGuard]  },
  { path: '', component: HomeComponent, pathMatch: 'full' }

];



@NgModule({
  declarations: [
    AppComponent,
    // NavMenuComponent,
    HomeComponent,
    TimersListComponent,
    StopwatchBarComponent,
    ProjectsComponent,
    ProjectDialogComponent,
    LabelsComponent,
    LabelsDialogComponent

  ], entryComponents: [ProjectDialogComponent, LabelsDialogComponent]
  ,
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatTableModule,
    MatButtonModule,
    MatGridListModule,
    MatChipsModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatInputModule,
    MatAutocompleteModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    MatMenuModule,
    MatDatepickerModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    DatePipe,
    TimerService,
    ProjectsService,
    LabelsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

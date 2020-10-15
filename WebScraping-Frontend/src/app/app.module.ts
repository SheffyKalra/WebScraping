import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WebScrapingComponent } from './Components/web-scraping/web-scraping.component';
import {StickyModule} from 'ng2-sticky-kit';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { ResizableModule } from 'angular-resizable-element';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes : Routes=[
      {
        path:'',
    component: WebScrapingComponent,
      },
      {
        path:'webscraping',
        component: WebScrapingComponent,
        pathMatch:'full'
      }

]

@NgModule({
  declarations: [
    AppComponent,
    WebScrapingComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    DragDropModule,
    ResizableModule,
    NgbModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    StickyModule,
    ReactiveFormsModule
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}

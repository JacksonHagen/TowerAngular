import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AuthModule } from "@auth0/auth0-angular";
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
		// NOTE import HttpClientModule after BrowserModule
		HttpClientModule,
		AuthModule.forRoot({
			domain: 'dev-wvrfwzhq.us.auth0.com',
			clientId: 'lAcTCVPJK7nDO2DAFlGznd09I1Z68b49'
		}),
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

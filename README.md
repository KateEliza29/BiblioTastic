# BiblioTastic  
## Access Information
[Access it here](https://bibliotastic.azurewebsites.net)

Authentication is through Microsoft Entra ID, through my tenancy. I've used the tenancy/Entra ID authentication method instead of public authentication using personal accounts because it reflects how organisations would manage company applications, and one of the aims of this project was to understand how organisations would host tenancy-restricted web apps. 
Because of the authentication model, access is through a tenancy email address and password:
Username: bibliotasticuser@kateeliza2901outlook.onmicrosoft.com 
Password: FrightHorrorScreamFest90&@@ 
Please don't set up MFA on this! It's a demo project and I'm keeping one set of credentials open on here so people can try out the site.

## Description
While in the middle of a master's module, we were asked to install a Bibliographic Database to keep track of our reading. I tested some and decided that they were either too complex for what I needed, or they didn't do what I wanted them to do, so I decided to make my own.  

This is the first release, with enough features to make it useful, but not with all the features that I'd like yet. It allows users to add/edit libraries and books/journal articles associated with the libraries. 

## Technical Composition  
On the very backend is an Azure SQL database. The API is a .NET 8 web api, hosted as an Azure App Service. The front end is written using Angular 17, and again, is hosted as an Azure App Service. 

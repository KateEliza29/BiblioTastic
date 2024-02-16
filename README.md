# BiblioTastic  
## Description
[Access it here](https://bibliotastic.azurewebsites.net)

While in the middle of a master's module, we were asked to install a Bibliographic Database to keep track of our reading. I tested some and decided that they were either too complex for what I needed, or they didn't do what I wanted them to do, so I decided to make my own.  

This is the first release, with enough features to make it useful, but not with all the features that I'd like yet. It allows users to add/edit libraries and books/journal articles associated with the libraries. 

## Technical Composition  
On the very backend is an Azure SQL database. The API is a .NET 8 web api, hosted as an Azure App Service. The front end is written using Angular 17, and again, is hosted as an Azure App Service. 

I've chosen to leave authentication out of this version to allow for public viewing. However, I am working on a version that uses Azure Entra ID to authenticate users. 

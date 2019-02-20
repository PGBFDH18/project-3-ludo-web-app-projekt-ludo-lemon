## CI/CD via Azure DevOps till Azure tillagt
Se separat dokumentation (docs/vg_cicd.md)

## Spelet är distribuerat till Webserver i Azure ##
https://ludowebapp-dev-as.azurewebsites.net

## Validering av inputdata
Vi har ordnat validering av player-name så att endast strängar kan matas in. Vidare tillåts enbart bokstäver a-z, A-Z.
Eftersom vi använder oss av drop-down-lister för färgval och för vilken pjäs (1-4) man vill flytta så är dessa input redan validerade.

## Lagring av spelinstanser
Påbörjade spel sparas i nuläget på webservern som är i gång hela tiden. Vi har även gjort en SQL-databas och vi jobbar på att implementera den och spara spelen där istället.

## Internationalisering
Vi har lagt till Internationalisering till vår applikation genom applicering av L10n (Localization) i vårat program. Programmet känner av språkinställningen för webbläsaren och anpassar automatiskt språket efter det. Vi har för närvarande språkstöd för:
* Engelska
* Svenska
* Finska
* Serbiska
* Ungerska
* Isländska

Danska kan läggas till om önskemål för det framkommer...


## ToDo's
- [x] Sätta upp och organisera SQL-database in *Gearhost*
- [x] Implementera kommunikation mellan databasen och API
- [x] Upprätta YAML-fil i *VisualStudio* Code alt. *SwaggerHub*
- [x] Upprätta API:et i VisualStudio
- [x] Lägga till CI/CD vi Azure DevOps
- [x] Implementera en WebbApp
- [x] Deploy till Azure webbserver
- [x] Validering av input
- [x] Loggning av vad som händer i spelet
- [x] Implementera stöd för flera språk i programmet

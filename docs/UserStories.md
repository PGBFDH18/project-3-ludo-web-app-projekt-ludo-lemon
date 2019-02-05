# Userstories
## WebAPP
- Som spelare vill jag se ett **välkomst-Webb-fönster** där spelet presenteras.
- Som spelare vill jag kunna logga in med ett **användarnamn**.
- Som spelare vill jag kunna få en lista på tidigare sparade spel där jag deltagit som spelare så att jag kan välja att **öppna/fortsätta** ett spel.
- Som spelare vill jag komma till ett nytt Webb-fönster där det sparade spelet **presenteras grafiskt** så att det skall gå att fortsätta spela den.
- Som spelare vill jag kunna välja att påbörja ett **nytt spel** i välkomst-fönstret och **namnge** spelet.
- Som spelare vill jag kunna välja bland **4 färger**.
- Som spelare vill jag kunna **påbörja spelet** när minst två spelare är med genom att tärningen kastas av första spelaren.
- Spelet påbörjas automatiskt när 4 spelare gått med genom att tärningen kastas av första spelaren.
- Som spelare vill jag komma till ett Webb-fönster så att jag grafiskt kan se spelplanen, pjäsernas placering och tärningen.
- Som spelare vill jag kunna **"kasta" tärningen** genom att klicka på den.
- Som spelare vill jag kunna se **resultatet av tärningsslaget** så att jag kan ta ställning till vilken pjäs jag vill/kan flytta.
- Som spelare vill jag kunna se **positionen på samtliga pjäser** på spelplanet så att jag kan ta ställning till vilken pjäs jag vill/kan flytta.
- Som spelare vill jag kunna välja att **flytta ut en pjäs från boet** om jag kastar **1** eller **6** med tärningen.
- Som spelare vill jag kunna **välja vilken av mina pjäser jag vill flytta** genom att klicka på den.
- Som spelare vill jag kunna **"knuffa ut"** en motståndares pjäs om jag hamnar på samma position som motståndaren. Den utslagna pjäsen placeras automatiskt i sin spelares bo.
- Som spelare kan jag inte bli "knuffad" om jag befinner mig på ruta **53-57** på spelplanet.
- Som spelare har min pjäs gått i mål när pjäsen flyttat **58 steg**.
- Som spelare har jag **vunnit** om alla mina fyra pjäser gått i mål först av alla deltagande spelare.
- Spelet **avslutas automatiskt** om en spelare vunnit.
- Som spelare vill jag kunna **avsluta ett spel** när jag vill.
- Som spelare vill jag kunna välja att **spara ett spel** som inte är avslutat så att det kan återupptas senare om jag vill.
- Som spelare vill jag kunna **namnge** spelet som skall sparas så att jag kan välja att fortsätta spela den senare om jag vill.

## api-interface
<p>Resources och hur olika metoder påverkar dem</p>
<table>
<thead>
<tr>
<th></th>
<th>GET<br>read</th>
<th>POST<br>create</th>
<th>PUT<br>update</th>
<th>DELETE<br>delete</th>
</tr>
</thead>
<tbody>
<tr>
<td>/ludo</td>
<td>Lista över sparade Ludospel</td>
<td>Skapa ett nytt spel</td>
<td>N/A</td>
<td>N/A</td>
</tr>
<tr>
<td>/ludo/{gameId}</td>
<td>Detaljeret information om spelet, vart alla pjäser står</td>
<td>Starta ett sparat spel</td>
<td>N/A</td>
<td>Ta bort ett sparat spel</td>
</tr>
<tr>
<td>/ludo/{gameId}/save</td>
<td>N/A</td>
<td>Spara ett spel</td>
<td>N/A</td>
<td>N/A</td>
</tr>
<tr>
<td>/ludo/{gameId}/players</td>
<td>N/A</td>
<td>Ny spelare skapas</td>
<td>N/A</td>
<td>N/A</td>
</tr>
<tr>
<td>/ludo/{gameId}/players/{playerId}/dice</td>
<td>Visa vad tärningsslaget visade</td>
<td>N/A</td>
<td>N/A</td>
<td>N/A</td>
</tr>
<tr>
<td>/ludo/{gameId}/players/{playerId}/{piece}</td>
<td>N/A</td>
<td>Välja vilken pjäs man vill flytta</td>
<td>N/A</td>
<td>N/A</td>
</tr>
</tbody>
</table>
</p>

## Tester i Postman
<p>Samtliga våra resources och deras metoder enligt tablell ovan har testats i en collection i Postman. Denna collection delas av alla medlemmar i projektgruppen.</p>
https://fantasticappleludo.postman.co/workspaces?type=personal</p>

## CI via Azure DevOps tillagt
## Lagring av databas
<p>Vi skapade en SQL-databas med tabeller och joinade med foreign keys. När databasen skulle användas fick vi dock problem vid inläsning med datatypen Dynamic varför vi valde att spara datan i en Json-fil på användarens skrivbord istället. Databasen liksom metoderna för att spara och hämta speldata från den finns alltjämt kvar så vi har möjlighet att fixa till detta framöver.</p>

## Förklaringar av speciella begrepp
### correctionFactor

- Varje pjäs har en "lokal" position (antal steg som pjäsen har gått) och en "global" position (vilken ruta på brädan pjäsen står på). De första fyra pjäserna som skapas för en spelare får correctionFactor = 0. Därefter ökas värdet med 10 för varje fjärde pjäs (0, 10, 20, 30). Om vi antar att en pjäs har correctionFactor = 20 och den har gått 16 steg kan vi räkna ut den absoluta positionen (var på brädan den är) genom att ta position + correctionFactor = 20 + 16 = 36. Den befinner sig alltså på ruta 36. Genom att räkna ut den absoluta/globala positionen kan vi jämföra två pjäser med varandra för att avgöra om de står på samma ruta.
Sammanfattat är correctionFactor en "konstant" som adderas med pjäsens lokala position för att få fram dess globala position.

## ToDo's
- [x] Sätta upp och organisera SQL-database in *Gearhost* <br>
- [x] Upprätta YAML-fil i *VisualStudio* Code alt. *SwaggerHub*
- [x] Upprätta API:et i VisualStudio
- [x] Lägga till CI vi Azure DevOps
- [ ] Refactoring Console till *GameEngine*
- [ ] Göra en Console applikation som kör Fia via Web-API

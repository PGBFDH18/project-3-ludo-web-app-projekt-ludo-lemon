# Userstories
## GameEngine
- Som en av *2-4* användare vill jag kunna välja färg.
- Nästa spelare i ordning ska kunna välja färg, en av kvarvarande tre, o.s.v.
- Innan spelat påbörjas, är alla spelares pjäser placerade i sina bon.
- När minst 2 spelare valts kan spelet påbörjas genom att tärningen kastas av första spelaren.
- Som spelare vill jag kunna se positionen på samtliga pjäser.
- Om en spelare slår *1* eller *6* med tärningen får hen välja att gå ut med en pjäs från boet.
- Som spelare vill jag kunna välja vilken av mina pjäser jag vill flytta.
- Om min pjäs hamnar på samma position som någon av de andra spelarnas pjäser kommer jag knuffa ut den andra pjäsen ("knuff").
- Den utslagna pjäsen placeras i sin spelares bo.
- På ruta **41-45** kan man inte bli "knuffad".
- En pjäs går i mål när spelaren flyttat den **45 steg**.
- Den spelare som först får alla sina pjäser i mål har vunnit spelet.
- Som användare ska jag kunna spara ett spel till en fil och återuppta det genom inläsning av filen.

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

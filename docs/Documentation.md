## CI/CD via Azure DevOps till Azure tillagt
Se separat dokumentation (docs/vg_cicd.md)

## Lagring av databas
<p>Vi skapade en SQL-databas med tabeller och joinade med foreign keys. När databasen skulle användas fick vi dock problem vid inläsning med datatypen Dynamic varför vi valde att spara datan i en Json-fil på användarens skrivbord istället. Databasen liksom metoderna för att spara och hämta speldata från den finns alltjämt kvar så vi har möjlighet att fixa till detta framöver.</p>

## Förklaringar av speciella begrepp


## ToDo's
- [x] Sätta upp och organisera SQL-database in *Gearhost* <br>
- [ ] Implementera kommunikation mellan databasen och API
- [x] Upprätta YAML-fil i *VisualStudio* Code alt. *SwaggerHub*
- [x] Upprätta API:et i VisualStudio
- [x] Lägga till CI/CD vi Azure DevOps
- [ ] Implementera en WebbApp
- [x] Deploy till Azure webbserver
- [ ] Validering av input
- [x] Loggning av vad som händer i spelet
- [ ] Minst 3 enhetstester

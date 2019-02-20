# Användarmanual

Starta fia spelet genom att klicka på länken https://ludowebapp-dev-as.azurewebsites.net

På välkomstsidan som öppnas finns en kort beskrivning av Fia-spelet. På denna sida skall man välja om man vill **starta ett nytt spel** eller **öppna ett sparat spel**.

Väljer man att **öppna ett sparat** spel så kommer man till en ny sida där samtliga påbörjade spel listas. Varje sparat spel har ett individuellt ID-nummer. Man kan här välja vilket av dom sparade spelen man vill fortsätta spela. Genom att klicka på "Join this game" kommer man till själva spelbrädan. Man har även möjlighet att radera spel i detta läge. Skulle det inte finnas några sparade spel, kommer man kunna länka sig till **starta ett nytt spel**.

Väljer man istället att **starta ett nytt spel** så öpnas ett fönster där man kan skriva in *1-4* användarnamn och från en drop-down-lista välja färg på sina respektive pjäser. När minst 2 spelare är inmatade kan man klicka på "Go to gameboard" varvid man kommer till själva spelbrädan.

I spelfönstret för spelbrädan ser man:
- spelbrädan med pjäserna utplacerade
- vilken spelare som står på tur. Användarnamnet visas i den färg som man tidigare valt.
- tärningen och värdet på tärningsslaget efter att tärningen är kastad

Här finns också:
- en knapp för att kasta tärningen
- en drop-down-lista för att välja vilken av pjäserna (1-4) man vill flytta
- en knapp för att genomföra själva flytten av den valda pjäsen.

Spelet påbörjas med att man kastar tärningen genom att klicka på "Roll THE Dice". Sedan väljer man i drop-down-listan den pjäs (1-4) som man vill gå med. Därefter klickar man på "Move Piece".
Automatiskt blir det nästa spelares tur att kasta tärningen o.s.v.

Vinner gör den spelare som först flyttat alla sina pjäser ett varv runt spelbrädet, in på sin målsträcka (spelarens färg) och in i mål.
Då en spelare vunnit, meddelas detta och spelet är slut.

Spelet sparas automatiskt efter varje pjäsflytt varför det inte finns någon "save"-knapp. Stänger man ner spelet så kan man senare välja att fortsätta spela genom att i läget för att "öppna ett sparat spel" välja GameID't för det spel man spelade.

# Item Sorter Robot (Week 7)

Dette projekt bygger videre på uge 6-opgaven med lager og ordrer.  
Formålet er at sende URScript-programmer til en robot-simulator, som sorterer varer fra position a, b og c til en fælles placering S.

## Formål
Programmet viser, hvordan man kan kombinere et simpelt lager- og ordresystem med grundlæggende robotstyring.  
Robotten henter varer fra faste positioner (a, b, c) og flytter dem til position S ved hjælp af URScript.

## Funktioner
- Genbruger modelklasser fra uge 6: `Item`, `UnitItem`, `BulkItem`, `Order`, `Inventory` og `OrderBook`.
- Opretter testordrer og genererer et lille URScript-program for hver countable vare.
- Viser URScript-koden i konsollen (eller sender den til robot-simulatoren, hvis den kører).
- Højst tre countable varer per ordre bliver behandlet, som opgaven kræver.

## Sådan køres programmet
1. Åbn projektmappen i terminalen.
2. Kør:
   ```bash
   dotnet run --project InventorySystemRobotControl/InventorySystemRobotControl.csproj

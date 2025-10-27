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

## Flowchart
flowchart TD
    A[Start] --> B[Init: Inventory + domain classes]
    B --> C[Create items (UnitItem, BulkItem)<br/>Assign Id: 1=a, 2=b, 3=c]
    C --> D[Add stock to Inventory]
    D --> E[Create test Orders]
    E --> F[Queue orders in OrderBook]
    F --> G{More queued orders?}
    G -- No --> Z[Exit]
    G -- Yes --> H[ProcessNextOrder(inv)]
    H --> I[Take next Order]
    I --> J[Filter to unit lines]
    J --> K{Processed < 3?}
    K -- No --> R[Finish order<br/>Update revenue]
    K -- Yes --> L[Map Item.Id → a/b/c coords<br/>(Coordinates.SourceByItemId)]
    L --> M[Set target = S()]
    M --> N[Generate URScript<br/>(UrScript.MakePickPlace)]
    N --> O{Simulator available?}
    O -- Yes --> P[Robot.SendProgram(script, item_id)]
    O -- No --> Q[Print script to console]
    P --> S[Update inventory; add to processed]
    Q --> S
    S --> G

using InventorySystemRobotControl;
using InventorySystemRobotControl.Domain;

// 1) Setup inventory
var inv = new Inventory();

var kaktus = new UnitItem("Kaktus", 49.95, 120) { Id = 1 };
var bonsai = new UnitItem("Bonsai-træ", 249.00, 850) { Id = 2 };
var jord   = new BulkItem("Pottemuld", 15.00, "kg")   { Id = 3 };

inv.Add(kaktus, 25);
inv.Add(bonsai, 10);
inv.Add(jord,   100);

// 2) Order book + customer
var book = new OrderBook();
var kunde = new Customer("Asha Sharma");

// 3) Order 1
var o1 = new Order();
o1.OrderLines.Add(new OrderLine(kaktus, 1));
o1.OrderLines.Add(new OrderLine(bonsai, 1));
o1.OrderLines.Add(new OrderLine(jord,   5)); // bulk
kunde.CreateOrder(o1);
book.QueueOrder(o1);

// 4) Order 2
var o2 = new Order();
o2.OrderLines.Add(new OrderLine(kaktus, 2));
o2.OrderLines.Add(new OrderLine(bonsai, 1));
kunde.CreateOrder(o2);
book.QueueOrder(o2);

// 5) Process all queued orders (simuler robot)
var (tx, ty, tz) = Coordinates.S();
var robot = new Robot(); // du sender ikke endnu

Console.WriteLine("=== Processing all orders ===");

while (book.queuedOrders.Count > 0)
{
    var next = book.ProcessNextOrder(inv);
    if (next == null) break;

    Console.WriteLine($"\n➡️  Processing new order ({next.OrderLines.Count} lines): {next.LinesText}");

    int processed = 0;
    foreach (var line in next.UnitLines())
    {
        if (processed >= 3) break; // krav: maks 3 countable items
        processed++;

        var (sx, sy, sz) = Coordinates.SourceByItemId(line.Item.Id);
        var script = UrScript.MakePickPlace(sx, sy, sz, tx, ty, tz);

        Console.WriteLine($"\n--- URScript for {line.Item.Name} ---");
        Console.WriteLine(script);
        robot.SendProgram(script, line.Item.Id);
    }

    Console.WriteLine($"✅ Order processed. Total revenue: {book.TotalRevenue():N2} kr.");
}

Console.WriteLine("\n=== Done processing all orders ===");
Console.WriteLine($"Remaining inventory:");
foreach (var s in inv.Stock)
    Console.WriteLine($"- {s.Item.Name}: {s.Amount}");

namespace FlorarieSOLID;

public class Flower {
    public string Name { get; }
    public double Price { get; }

    public Flower(string name, double price) {
        Name = name;
        Price = price;
    }
}

public class FlowerOrder {
    private readonly List<(Flower Flower, int Quantity)> items = new();

    public void AddFlower(Flower flower, int quantity) {
        items.Add((flower, quantity));
    }

    public double GetTotal() => items.Sum(item => item.Flower.Price * item.Quantity);

    public IReadOnlyList<(Flower Flower, int Quantity)> Items => items;
}

public class InvoicePrinter {
    public void PrintSummary(FlowerOrder order, IDeliveryCost delivery, string paymentMethod) {
        double deliveryCost = delivery.CalculateCost();
        double finalTotal = order.GetTotal() + deliveryCost;

        System.Console.WriteLine("\n=== SUMAR COMANDA ===");
        foreach (var item in order.Items) {
            System.Console.WriteLine($"✓ {item.Quantity} x {item.Flower.Name} - {item.Flower.Price * item.Quantity} lei");
        }
        System.Console.WriteLine($"Total flori: {order.GetTotal()} lei");
        System.Console.WriteLine($"✓ Livrare: {deliveryCost} lei");
        System.Console.WriteLine($"✓ Plata: {paymentMethod}");
        System.Console.WriteLine($"SUMA FINALA: {finalTotal} lei");
        System.Console.WriteLine("✓ Comanda a fost înregistrată cu succes!");
    }
}
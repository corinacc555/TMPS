namespace FlorarieSOLID;
using System;

public class InvoicePrinter 
{
    public void PrintInvoice(FlowerOrder order) 
    {
        Console.WriteLine("\n=== FACTURA COMENZII ===");
        foreach(var item in order.Items) 
        {
            Console.WriteLine($"{item.Quantity} x {item.SelectedFlower.Name} ({item.SelectedFlower.Price} lei/buc) = {item.GetItemTotal()} lei");
        }
        
        if (order.Delivery != null) 
        {
            Console.WriteLine($"Cost Livrare: {order.Delivery.CalculateCost()} lei");
        }
        
        Console.WriteLine("------------------------");
        Console.WriteLine($"TOTAL DE PLATĂ: {order.GetTotal()} lei");
        Console.WriteLine("========================\n");
    }
}
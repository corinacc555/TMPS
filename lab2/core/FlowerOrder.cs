namespace FlorarieSOLID;
using System.Collections.Generic;

public class FlowerOrder 
{
    public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
    public IDeliveryCost Delivery { get; set; }

    public void AddFlower(Flower flower, int quantity) 
    {
        Items.Add(new OrderItem(flower, quantity));
    }

    public double GetTotal() 
    {
        double total = 0;
        foreach(var item in Items) total += item.GetItemTotal();
        if (Delivery != null) total += Delivery.CalculateCost();
        return total;
    }
}
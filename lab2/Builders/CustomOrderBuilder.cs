namespace FlorarieSOLID;

public class CustomOrderBuilder : IOrderBuilder
{
    private FlowerOrder _order = new FlowerOrder();

    public void BuildRoses(int count) 
    {
        _order.AddFlower(new Flower("Trandafir", 50.0), count);
    }

    public void BuildTulips(int count) 
    {
        _order.AddFlower(new Flower("Lalea", 20.0), count);
    }

    public void BuildDelivery(IDeliveryCost delivery) 
    {
        _order.Delivery = delivery;
    }
    
    public FlowerOrder GetResult() => _order;
}
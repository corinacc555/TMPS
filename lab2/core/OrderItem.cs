namespace FlorarieSOLID;

public class OrderItem 
{ 
    public Flower SelectedFlower { get; set; }
    public int Quantity { get; set; }
    
    public OrderItem(Flower flower, int quantity) 
    { 
        SelectedFlower = flower; 
        Quantity = quantity; 
    }
    
    public double GetItemTotal() => SelectedFlower.Price * Quantity;
}
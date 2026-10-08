namespace FlorarieSOLID;

public interface ICardPayment { void PayWithCard(); }
public interface ICashPayment { void PayWithCash(); }

public class InStoreOrder : ICardPayment, ICashPayment {
    public void PayWithCard() => System.Console.WriteLine("Card OK.");
    public void PayWithCash() => System.Console.WriteLine("Cash OK.");
}

public class OnlineOrder : ICardPayment {
    public void PayWithCard() => System.Console.WriteLine("Card OK online.");
}
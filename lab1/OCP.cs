namespace FlorarieSOLID;

public interface IDeliveryCost {
    double CalculateCost();
}

public class StandardDelivery : IDeliveryCost {
    public double CalculateCost() => 15.0;
}

public class ExpressDelivery : IDeliveryCost {
    public double CalculateCost() => 30.0;
}
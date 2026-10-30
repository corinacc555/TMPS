namespace FlorarieSOLID;

public abstract class DeliveryCreator
{
    public abstract IDeliveryCost FactoryMethod();
}

public class StandardDeliveryCreator : DeliveryCreator
{
    public override IDeliveryCost FactoryMethod() => new StandardDelivery();
}

public class ExpressDeliveryCreator : DeliveryCreator
{
    public override IDeliveryCost FactoryMethod() => new ExpressDelivery();
}
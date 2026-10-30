namespace FlorarieSOLID;

public class OrderDirector
{
    public void ConstructValentinesBouquet(IOrderBuilder builder)
    {
        builder.BuildRoses(11); 
        builder.BuildDelivery(new ExpressDeliveryCreator().FactoryMethod()); 
    }
}
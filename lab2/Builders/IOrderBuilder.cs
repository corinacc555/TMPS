namespace FlorarieSOLID;

public interface IOrderBuilder
{
    void BuildRoses(int count);
    void BuildTulips(int count);
    void BuildDelivery(IDeliveryCost delivery);
    FlowerOrder GetResult();
}
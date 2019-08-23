namespace Domain.Interfaces
{
    public interface IPricingStrategy
    {
        double GetPrice(double rawPrice);
    }
}

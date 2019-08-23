namespace Domain.Interfaces
{
    public interface IPricingStrategyManager
    {
        IPricingStrategy GetPricingStrategy(char supply, char demand);
    }
}

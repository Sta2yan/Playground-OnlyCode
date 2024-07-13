namespace Agava.Shop
{
    public interface IItemsWallet
    {
        bool CanSpend(string id, int count);
        void Spend(string id, int count);
    }
}

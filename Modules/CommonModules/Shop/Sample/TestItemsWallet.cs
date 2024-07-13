namespace Agava.Shop.Sample
{
    public class TestItemsWallet : IItemsWallet
    {
        public bool CanSpend(string id, int count)
        {
            return true;
        }

        public void Spend(string id, int count)
        {
            
        }
    }
}
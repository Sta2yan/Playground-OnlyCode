namespace Agava.Shop
{
    public class PurchasedProduct
    {
        public PurchasedProduct(string id, int count)
        {
            Id = id;
            Count = count;
        }

        public string Id { get; }
        public int Count { get; }
    }
}

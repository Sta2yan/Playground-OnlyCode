namespace Agava.Shop
{
    public interface IShopView
    {
        void Render(ICatalog[] catalogs);
        void Close();
    }
}

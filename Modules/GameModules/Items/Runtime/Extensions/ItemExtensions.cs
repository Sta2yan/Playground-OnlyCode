namespace Agava.Playground3D.Items
{
    public static class ItemExtensions
    {
        public static bool TryConvertTo<TTargetItem>(this IItem item, out TTargetItem targetItem) where TTargetItem : IItem
        {
            targetItem = default;
            
            if (item is not TTargetItem block) 
                return false;
            
            targetItem = block;
            return true;
        }

        public static bool Is<TTargetItem>(this IItem item) where TTargetItem : IItem
        {
            return item is TTargetItem;
        }
    }
}

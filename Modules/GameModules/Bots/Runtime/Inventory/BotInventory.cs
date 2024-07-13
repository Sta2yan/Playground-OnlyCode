using UnityEngine;
using Agava.Playground3D.Items;
using Agava.Playground3D.Input;

namespace Agava.Playground3D.Bots
{
    internal class BotInventory : MonoBehaviour, IBotInventory
    {
        [SerializeField] private Hand _hand;
        [Header("Items")]
        [SerializeField] private Item _nullableItem;
        [SerializeField] private Item _sword;
        [SerializeField] private Item _block;
        [SerializeField] private Item _pickaxe;
        
        public void EquipWeapon()
        {
            _hand.ChangeItem(_sword);
        }

        public void EquipBlock() 
        {
            _hand.ChangeItem(_block);
        }

        public void Unequip()
        {
            _hand.ChangeItem(_nullableItem);
        }
        
        public void EquipPickaxe()
        {
            _hand.ChangeItem(_pickaxe);
        }
    }
}

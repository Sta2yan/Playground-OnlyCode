using Agava.Playground3D.Items;
using System;
using UnityEngine;
using System.Linq;

namespace Agava.ExperienceSystem
{
    [CreateAssetMenu(fileName = "ItemExperienceEventRule", menuName = "ExperienceEventRules/Create ItemExperienceEventRule", order = 52)]
    public class ItemExperienceEventRule : ScriptableObject, IItemExperienceEventRule
    {
        [SerializeField] private ExperienceEventRule _defaultExperienceEventRule;
        [SerializeField] private ItemExperienceRule[] _itemExperienceRules;

        public bool TryGetExperienceEvent(IItem item, out ExperienceEvent experienceEvent)
        {
            experienceEvent = null;
            ItemExperienceRule itemExperienceRule = _itemExperienceRules.FirstOrDefault(rule => rule.Item.Id == item.Id);

            if (itemExperienceRule != null)
            {
                experienceEvent = new ExperienceEvent(itemExperienceRule.Experience);
            }
            else
            {
                if (_defaultExperienceEventRule == null)
                    return false;

                experienceEvent = _defaultExperienceEventRule.ExperienceEvent();
            }

            return true;
        }
    }

    [Serializable]
    internal class ItemExperienceRule
    {
        [field: SerializeField] public Item Item { get; private set; }
        [field: SerializeField, Min(1)] public int Experience { get; private set; }
    }
}

using Agava.ExperienceSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.MainMenu
{
    public class LevelGatePortalContainer : MonoBehaviour, ILevelGateContentContainer
    {
        [SerializeField] private PortalToMode _unlockedPortal;

        public bool TryUnlockContent(LockedItemsList lockedItemsList, int playerLevel, out List<ILevelGateContent> unlockedContent, bool instaUnlock = false)
        {
            unlockedContent = new();

            if (lockedItemsList.ItemExists(_unlockedPortal))
            {
                if (_unlockedPortal.TryUnlock(playerLevel, instaUnlock))
                    unlockedContent.Add(_unlockedPortal);
            }

            return unlockedContent.Count > 0;
        }
    }
}

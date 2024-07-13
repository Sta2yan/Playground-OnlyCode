using System;
using UnityEngine;

namespace Agava.Playground3D.MainMenu
{
    public class PortalBotTargetSpot : MonoBehaviour
    {
        [SerializeField] private PortalToMode _portal;

        public bool PortalUnlocked => _portal.Unlocked;
    }
}

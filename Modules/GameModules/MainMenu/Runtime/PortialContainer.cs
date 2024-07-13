using UnityEngine;

namespace Agava.Playground3D.MainMenu
{
    public class PortialContainer : MonoBehaviour
    {
        [SerializeField] private PortalToMode _bedWarsPortal;
        [SerializeField] private PortalToMode _sandboxPortal;
        [SerializeField] private PortalToMode _onlyUpPortal;
        [SerializeField] private PortalToMode _obbyPortal;

        public void Initialize(GameObject modesPanel, GameObject bedWarsTargetMode, GameObject sandboxTargetMode, GameObject onlyUpTargetMode, GameObject obbyTargetMode)
        {
            _bedWarsPortal.Initialize(bedWarsTargetMode, modesPanel);
            _sandboxPortal.Initialize(sandboxTargetMode, modesPanel);
            _onlyUpPortal.Initialize(onlyUpTargetMode, modesPanel);
            _obbyPortal.Initialize(obbyTargetMode, modesPanel);
        }
    }
}

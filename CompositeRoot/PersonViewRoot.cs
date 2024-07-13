using System.Collections.Generic;
using Agava.AdditionalPredefinedMethods;
using Agava.Customization;
using Agava.Movement;
using Agava.Playground3D.Input;
using UnityEngine;

namespace Agava.Playground3D.CompositeRoot
{
    public class PersonViewRoot : CompositeRoot
    {
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private List<SkinList> _skinLists;

        private IGameLoop _gameLateLoop;
        
        public override void Compose()
        {
            var personView = new PersonViewControl(_cameraMovement, _skinLists.ToArray());

            _gameLateLoop = new GameLoopGroup(personView);
        }

        private void LateUpdate()
        {
            _gameLateLoop?.Update(Time.deltaTime);
        }
    }
}

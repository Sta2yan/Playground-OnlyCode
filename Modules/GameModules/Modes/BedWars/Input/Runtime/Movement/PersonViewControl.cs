using Agava.AdditionalPredefinedMethods;
using Agava.Customization;
using Agava.Movement;

namespace Agava.Playground3D.Input
{
    internal class PersonViewControl : IGameLoop
    {
        private readonly CameraMovement _cameraMovement;
        private readonly SkinList[] _skinLists;

        public PersonViewControl(CameraMovement cameraMovement, SkinList[] skinLists)
        {
            _cameraMovement = cameraMovement;
            _skinLists = skinLists;
        }

        public void Update(float _)
        {
            if (_cameraMovement.FirstPersonPerspective)
                foreach (var skinList in _skinLists)
                    skinList.DisablePartsThirdPerson();
            else if (_cameraMovement.FirstPersonPerspective == false)
                foreach (var skinList in _skinLists)
                    skinList.EnablePartsThirdPerson();
        }
    }
}

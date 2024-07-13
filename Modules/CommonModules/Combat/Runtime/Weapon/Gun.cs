using System;
using UnityEngine;

namespace Agava.Combat
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] private float _pushForce;
        [SerializeField] private float _reloadTime;
        [SerializeField] private HandTypeGun _handType;
        [SerializeField] private Bullet _bulletTemplate;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Sprite _crosshair;
        [SerializeField] private GunAnimator _gunAnimator;
        [SerializeField] private LayerMask _ignoredLayer;

        private float _currentReloadTime;
        private bool _isMobile;
        private IMagazine _magazine;
        private ICombatCharacter _without;
        public bool _isRedBlood = true;

        public bool Initialized => _magazine != null;
        public bool CanShot => _magazine.Has() && _currentReloadTime >= _reloadTime;
        public float CurrentReloadTime => _currentReloadTime;
        public HandTypeGun HandType => _handType;
        public Sprite Crosshair => _crosshair;
        public bool IsRedBlood => _isRedBlood;

        public void Initialize(IMagazine magazine, ICombatCharacter without, bool isMobile)
        {
            if (Initialized)
                throw new InvalidOperationException("Already initialize");

            _magazine = magazine;
            _without = without;
            _currentReloadTime = _reloadTime;
            _isMobile = isMobile;
        }

        private void Update()
        {
            if (_currentReloadTime < _reloadTime)
                _currentReloadTime += Time.deltaTime;
        }

        public void Shot(bool isRedBlood, Action onKill = null, Action onHit = null)
        {
            _isRedBlood = isRedBlood;
            AdditionalShotRules(onKill, onHit);

            if (_gunAnimator != null)
                _gunAnimator.Shoot();

        }

        protected virtual void AdditionalShotRules(Action onKill = null, Action onHit = null)
        {
            if (CanShot == false)
                throw new InvalidOperationException();

            DefaultShot(onKill, onHit);
        }

        protected virtual Vector3 AdditionalDirectionRules()
        {
            return DirectionShot();
        }

        protected void DefaultShot(Action onKill = null, Action onHit = null)
        {
            var bulletInstance = Instantiate(_bulletTemplate, _bulletSpawnPoint.position, Quaternion.identity);

            var direction = AdditionalDirectionRules();

            bulletInstance.transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
            bulletInstance.Push(direction * _pushForce, _without, _isRedBlood, onKill, onHit);

#if UNITY_EDITOR
            VisualizeShotLine(direction * _pushForce);
#endif

            _currentReloadTime = 0;
            _magazine.Remove();
        }

        private Vector3 DirectionShot()
        {
            Camera cameraMain = Camera.main;
            Vector3 mousePosition = Input.mousePosition;

            Ray ray;
            //if (_isMobile)
            //{
            //    float x = Screen.width / 2f;
            //    float y = Screen.height / 2f;
            //    mousePosition = new Vector3(x, y, cameraMain.transform.position.z);
            //    ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));

            //}
            //else
            //{
            mousePosition.z = cameraMain.transform.position.z;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //}

            Vector3 worldPosition = cameraMain.ScreenToWorldPoint(mousePosition);



            int layerMaskWithoutPlayer = ~_ignoredLayer;
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMaskWithoutPlayer))
                worldPosition = hit.point;

            Vector3 direction = (worldPosition - _bulletSpawnPoint.position).normalized;

            return direction;
        }

#if UNITY_EDITOR
        private void VisualizeShotLine(Vector3 direction)
        {
            Debug.DrawRay(_bulletSpawnPoint.position, direction * 1000f, Color.red, 1f);
        }
#endif
    }
}

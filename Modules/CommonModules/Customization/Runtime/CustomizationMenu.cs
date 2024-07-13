using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Agava.Customization
{
    public class CustomizationMenu : MonoBehaviour
    {
        [SerializeField] private float _cameraAnimationDuration;
        [SerializeField] private SkinSelect _skinSelect;
        [SerializeField] private List<SkinButton> _skinButtons;
        [SerializeField] private Camera _cameraCustomization;
        [SerializeField] private Camera _cameraMain;
        [SerializeField] private GameObject _panelRotateModel;
        [SerializeField] private GameObject _movementRoot;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Tooltip("Is ISkinCustomizationSelect")] private MonoBehaviour _skinCustomizationSelect;

        private ISkinCustomizationSelect SkinCustomizationSelect => (ISkinCustomizationSelect) _skinCustomizationSelect;

        private void OnValidate()
        {
            if (_skinCustomizationSelect is not ISkinCustomizationSelect)
            {
                _skinCustomizationSelect = null;
                throw new InvalidOperationException("SkinCustomizationSelect is not ISkinCustomizationSelect");
            }
        }

        public void Show()
        {
            foreach (var skinSelectButton in _skinButtons)
            {
                skinSelectButton.Clicked += OnSkinButtonClicked;
                skinSelectButton.Show();
            }

            OnSkinButtonClicked(_skinButtons[0]);
            SkinCustomizationSelect.Execute();
        }

        public void Hide()
        {
            foreach (var skinSelectButton in _skinButtons)
            {
                skinSelectButton.Clicked -= OnSkinButtonClicked;
                skinSelectButton.Hide();
            }
            
            _skinSelect.Hide();
            _cameraCustomization.gameObject.SetActive(false);
            _panelRotateModel.gameObject.SetActive(false);
            _cameraMain.gameObject.SetActive(true);
            _movementRoot.SetActive(true);
            _rigidbody.isKinematic = false;
        }
        
        internal void OnSkinButtonClicked(SkinButton skinButton)
        {
            if (_skinSelect.Showed)
                _skinSelect.Hide();

            foreach (var button in _skinButtons)
                button.RenderUnselect();
            
            skinButton.RenderSelect();
            _skinSelect.Show(skinButton.TargetSkinList);
            
            if (_cameraMain.gameObject.activeSelf)
            {
                _cameraCustomization.transform.position = _cameraMain.transform.position;
                _cameraCustomization.transform.rotation = _cameraMain.transform.rotation;
            }

            _cameraCustomization.gameObject.SetActive(true);
            _panelRotateModel.gameObject.SetActive(true);
            _cameraMain.gameObject.SetActive(false);
            _movementRoot.SetActive(false);
            _rigidbody.isKinematic = true;
            MoveCameraTo(skinButton.CameraTargetPoint.position, skinButton.CameraTargetPoint.rotation.eulerAngles);
        }

        private void MoveCameraTo(Vector3 position, Vector3 rotation)
        {
            _cameraCustomization.DOComplete();

            _cameraCustomization.transform.DOMove(position, _cameraAnimationDuration);
            _cameraCustomization.transform.DORotate(rotation, _cameraAnimationDuration);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Agava.ExperienceSystem;
using Agava.Audio;

namespace Agava.Playground3D.CoffeeBreak
{
    public class CoffeeBreakMiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Balloon[] _balloons;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _screenBorder;
        [SerializeField] private SoundSource _popSound;

        private const float FadingDuration = 0.5f;

        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _balloonPopEvent;

        public void Initialize(ExperienceEventsContainer experienceEventsContainer, ExperienceEventRule balloonPopEvent)
        {
            _experienceEventsContainer = experienceEventsContainer;
            _balloonPopEvent = balloonPopEvent;
        }

        public void StartMiniGame()
        {
            _canvas.SetActive(true);
            _background.DOFade(1f, FadingDuration).SetUpdate(true).OnComplete(
                () =>
                {
                    foreach (Balloon balloon in _balloons)
                    {
                        balloon.Show();
                        balloon.StartAnimation(_screenBorder.position, () =>
                        {
                            _experienceEventsContainer.TriggerEvent(_balloonPopEvent.ExperienceEvent());
                            _popSound.Play();
                        });
                    }
                });
            _text.DOFade(1f, FadingDuration).SetUpdate(true);
        }

        public void StopMiniGame()
        {
            _canvas.SetActive(false);
            _background.DOFade(0f, 0f).SetUpdate(true);
            _text.DOFade(0f, 0f).SetUpdate(true);

            foreach (Balloon balloon in _balloons)
            {
                balloon.Hide();
                balloon.StopAnimation();
            }
        }
    }
}

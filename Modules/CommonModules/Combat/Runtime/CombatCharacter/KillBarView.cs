using System.Collections;
using TMPro;
using UnityEngine;

namespace Agava.Combat
{
    public class KillBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _whoWasKilled;
        [SerializeField] private TMP_Text _whoKilled;
        [SerializeField] private CanvasGroup _panel;
        [SerializeField, Min(0)] private float _delayToDisable;

        private void Awake()
        {
            StartCoroutine(SlowDisablePanel());
        }

        public void Render(string whoWasKilled, string whoKilled)
        {
            _whoWasKilled.text = whoWasKilled;
            _whoKilled.text = whoKilled;
        }

        private IEnumerator SlowDisablePanel()
        {
            yield return new WaitForSeconds(_delayToDisable);

            while (_panel.alpha > 0)
            {
                _panel.alpha -= .01f;
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}

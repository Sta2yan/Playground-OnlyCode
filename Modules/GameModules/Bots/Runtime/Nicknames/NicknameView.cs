using UnityEngine;
using TMPro;

namespace Agava.Playground3D.Bots
{
    public class NicknameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nicknameText;

        public void Render(string nickname)
        {
            _nicknameText.text = nickname;
        }
    }
}

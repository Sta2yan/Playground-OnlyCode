using TMPro;
using UnityEngine;

namespace Agava.Combat
{
    public class KillsCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;

        private int _kills;

        public void Add(int countKills)
        {
            _kills += countKills;
            _count.text = _kills.ToString();
        }
    }
}

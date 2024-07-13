using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Playground3D.BedWars.Combat
{
    public class BedWarsTeamsView : MonoBehaviour
    {
        [SerializeField] private Sprite _aliveTeamBackground;
        [SerializeField] private Sprite _deadTeamBackground;
        [SerializeField] private TeamInfo[] _teams;

        private void Update()
        {
            foreach (var team in _teams)
            {
                bool hasBed = team.Team.HasBed;
                
                team.Background.sprite = hasBed ? _aliveTeamBackground : _deadTeamBackground;
                team.Background.color = team.Team.Color;
                team.AliveCharactersText.text = hasBed ? team.Team.Characters.ToString() : team.Team.AliveCharacters.ToString();
            }
        }

        [Serializable]
        private class TeamInfo
        {
            [field: SerializeField] public BedWarsTeam Team { get; private set; }
            [field: SerializeField] public Image Background { get; private set; }
            [field: SerializeField] public TMP_Text AliveCharactersText { get; private set; }
        }
    }
}
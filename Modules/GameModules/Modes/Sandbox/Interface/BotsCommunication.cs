using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agava.Playground3D.Sandbox.UserInterface
{
    public class BotsCommunication : MonoBehaviour
    {
        public bool Selected { get; private set; } = false;

        public void ClearBots()
        {
            Selected = true;
        }

        public void Unselect()
        {
            Selected = false;
        }
    }
}

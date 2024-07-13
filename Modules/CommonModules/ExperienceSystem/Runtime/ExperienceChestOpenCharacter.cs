using System;
using Agava.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Agava.ExperienceSystem
{
    public class ExperienceChestOpenCharacter : MonoBehaviour
    {
        [SerializeField] private LayerMask _without;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.MouseOverUI() == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit info, 1000f, _without))
                    {
                        if (info.collider.TryGetComponent(out ExperienceChest experienceChest))
                        {
                            experienceChest.TryOpen();
                        }
                    }
                }
            }
        }
    }
}

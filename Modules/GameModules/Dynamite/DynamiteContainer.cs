using System.Collections.Generic;
using Agava.Blocks;
using Agava.Combat;
using Agava.ExperienceSystem;
using UnityEngine;

namespace Agava.Playground3D.Dynamite
{
    public class DynamiteContainer : MonoBehaviour
    {
        private readonly Stack<Dynamite> _dynamites = new();

        private BlocksCommunication _blocksCommunication;
        private ExperienceEventsContainer _experienceEventsContainer;
        private ExperienceEventRule _dynamiteExplosionEventRule;

        private void Update()
        {
            if (_dynamites.Count <= 0)
                return;

            Dynamite dynamite = _dynamites.Pop();
            _experienceEventsContainer.TriggerEvent(_dynamiteExplosionEventRule.ExperienceEvent());

            foreach (var collider in dynamite.Colliders)
            {
                if (collider == null)
                    continue;

                if (collider.TryGetComponent(out Block block))
                {
                    Dynamite targetDynamite = block.GetComponentInChildren<Dynamite>();

                    if (targetDynamite != null)
                        targetDynamite.Activate(targetDynamite.ExplosionDelay);

                    _blocksCommunication.ApplyDamage(collider.bounds.center, dynamite.Damage, dynamite);
                }
                else if (collider.TryGetComponent(out CombatCharacter character))
                {
                    character.Hit(dynamite.Damage, dynamite.PushForce * Vector3.up);
                }
            }
        }

        public void Initialize(BlocksCommunication blocksCommunication,
            ExperienceEventsContainer experienceEventsContainer,
            ExperienceEventRule dynamiteExplosionEventRule)
        {
            _blocksCommunication = blocksCommunication;
            _experienceEventsContainer = experienceEventsContainer;
            _dynamiteExplosionEventRule = dynamiteExplosionEventRule;
        }

        public void AddDynamite(Dynamite dynamite)
        {
            _dynamites.Push(dynamite);
        }
    }
}
using Agava.Playground3D.NewYearEvent;
using BehaviorDesigner.Runtime;

namespace Agava.Playground3D.Bots
{
    internal class SharedCollectableItemsBotContainer : SharedVariable<CollectableItemsBotContainer>
    {
        public static implicit operator SharedCollectableItemsBotContainer(CollectableItemsBotContainer value) => new SharedCollectableItemsBotContainer { Value = value };
    }
}

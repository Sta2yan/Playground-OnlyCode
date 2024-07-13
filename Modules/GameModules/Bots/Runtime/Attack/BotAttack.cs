using Agava.Combat;
using Agava.Playground3D.Input;
using Agava.Playground3D.Items;

namespace Agava.Playground3D.Bots
{
    internal class BotAttack : IBotAttack
    {
        private readonly ICombatCharacter _character;
        private readonly ICombatAnimation _combatAnimation;
        private readonly Hand _hand;
        private readonly ITeamList _teamList;

        public BotAttack(ICombatCharacter character, ICombatAnimation combatAnimation, Hand hand, ITeamList teamList)
        {
            _character = character;
            _combatAnimation = combatAnimation;
            _hand = hand;
            _teamList = teamList;
        }

        public bool TryAttack()
        {
            if (ItemInHandIs<ISword, Sword>(out var sword))
            {
                if (TryUseSword(sword))
                {
                    _combatAnimation.Hit();
                    return true;
                }
            }

            return false;
        }

        private bool TryUseSword(Sword sword)
        {
            ITeam[] friendlyTeams;

            if (_teamList.TryGetFriendlyTeams(_character, out friendlyTeams) == false)
                friendlyTeams = new ITeam[] { };

            if (sword.CanAttack)
                sword.Attack(_character.Forward, without: new[] { _character }, friendlyTeams: friendlyTeams);

            return true;
        }

        private bool ItemInHandIs<TTargetItem, TTarget>(out TTarget targetItem) where TTargetItem : IItem where TTarget : class
        {
            targetItem = default;

            if (_hand.CurrentItem.Is<TTargetItem>() == false)
                return false;

            if (_hand.ItemInstance.TryGetComponent(out TTarget itemFound) == false)
                return false;

            targetItem = itemFound;
            return true;
        }
    }
}

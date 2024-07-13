using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    internal class FindClosestCombatCharacter : FindClosestTarget<CombatCharacter, SharedCombatCharacter>
    {
        public SharedBotEssensials BotEssensials;
        public SharedCombatCharacter SelfCombatCharacter;

        protected override bool IgnoreFoundObject(CombatCharacter foundObject)
        {
            BotEssensials sharedBotEssensials = BotEssensials.Value;
            CombatCharacter selfCombatCharacter = SelfCombatCharacter.Value;

            if (selfCombatCharacter == foundObject)
                return true;

            var teamList = sharedBotEssensials.TeamList;

            if (teamList.TryFindCharacterTeam(selfCombatCharacter, out ITeam selfTeam))
            {
                if (teamList.TryFindCharacterTeam(foundObject, out ITeam foundTeam))
                {
                    return selfTeam.FriendlyTeam(foundTeam);
                }
            }

            return false;
        }
    }
}

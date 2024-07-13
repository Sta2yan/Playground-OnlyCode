using Agava.Combat;

namespace Agava.Playground3D.Bots
{
    public class BotEssensials
    {
        public BotEssensials(IBotInputMimic inputMimic, ITeamList teamList)
        {
            InputMimic = inputMimic;
            TeamList = teamList;
        }

        public IBotInputMimic InputMimic { get; }
        public ITeamList TeamList { get; }
    }
}

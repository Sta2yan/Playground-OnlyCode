using Agava.Blocks;

namespace Agava.Playground3D.Bots
{
    internal class SandboxBotComposition : BotComposition<SandboxBotComposer>
    {
        private readonly BlocksCommunication _blocksCommunication;
        private readonly ISandboxTeamList _teamList;

        public SandboxBotComposition(BlocksCommunication blocksCommunication, ISandboxTeamList teamList) : base()
        {
            _blocksCommunication = blocksCommunication;
            _teamList = teamList;
        }

        protected override void InitializeBotComposer(SandboxBotComposer botComposer)
        {
            botComposer.Initialize(_blocksCommunication, _teamList);
        }
    }
}

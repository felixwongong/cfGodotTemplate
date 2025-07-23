using cfEngine.Util;

namespace cfGodotTemplate.Core
{
    public class LocalLoadState: GameState
    {
        public override GameStateId Id => GameStateId.LocalLoad;

        public override void StartContext(StateParam param)
        {
            StateMachine.ForceGoToState(GameStateId.InfoLoad);
        }
    }
}
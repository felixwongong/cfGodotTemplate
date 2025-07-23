using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Logging;
using cfEngine.Util;

namespace cfGodotTemplate.Core
{
    public class InitializationState : GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new() { GameStateId.UILoad };
        public override GameStateId Id => GameStateId.Initialization;

        public override void StartContext(StateParam stateParam)
        {
            Initialize().ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    StateMachine.TryGoToState(GameStateId.UILoad);
                }
                else
                {
                    Log.LogException(t.Exception);
                }
            });
        }

        private Task Initialize()
        {
            return Task.CompletedTask;
        }
    }
}

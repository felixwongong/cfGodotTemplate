using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Extension;
using cfEngine.Logging;
using cfEngine.Util;

namespace cfGodotTemplate.Core
{
    public class UILoadState: GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new()
        {
            GameStateId.Initialization
        };

        public override GameStateId Id => GameStateId.UILoad;

        public override void StartContext(StateParam param)
        {
            var loadTaskList = new List<Task>
            {
                Task.CompletedTask
            };

            Task.WhenAll(loadTaskList)
                .ContinueWithSynchronized(t =>
                {
                    if (t.IsFaulted)
                    {
                        Log.LogException(t.Exception);
                        return;
                    }
                    StateMachine.ForceGoToState(GameStateId.BootstrapEnd);
                });
        }
    }
}
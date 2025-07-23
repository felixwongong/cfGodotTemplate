using cfEngine.Service;
using cfEngine.Util;

namespace cfEngine.Core
{
    using cfGodotTemplate.Core;
    public static partial class GameExtension
    {
        public static Game WithGameStateMachine(this Game game, GameStateMachine service)
        {
            game.Register(service, nameof(GameStateMachine));
            return game;
        }

        public static GameStateMachine GetGameStateMachine(this Game game) => game.GetService<GameStateMachine>(nameof(GameStateMachine));
    }
}

namespace cfGodotTemplate.Core
{
    public enum GameStateId
    {
        LocalLoad,
        InfoLoad,
        Login,
        UserDataLoad,
        Initialization,
        UILoad,
        BootstrapEnd,
    }

    public abstract class GameState : State<GameStateId, GameState, GameStateMachine>
    {
    }

    public class GameStateMachine : StateMachine<GameStateId, GameState, GameStateMachine>, IService
    {
        public GameStateMachine() : base()
        {
            RegisterState(new LocalLoadState());
            RegisterState(new InfoLoadState());
            RegisterState(new LoginState());
            RegisterState(new UserDataLoadState());
            RegisterState(new InitializationState());
            RegisterState(new UILoadState());
            RegisterState(new BootstrapEndState());
        }
    }
}

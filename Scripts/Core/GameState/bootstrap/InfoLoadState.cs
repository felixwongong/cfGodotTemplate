using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Core;
using cfEngine.Extension;
using cfEngine.Info;
using cfEngine.IO;
using cfEngine.Serialize;
using cfEngine.Service;
using cfEngine.Util;
using cfEngine.Service.Auth;
using cfGodotTemplate.Info;
using cfGodotTemplate.Util;

namespace cfGodotTemplate.Core
{
    public class InfoLoadState : GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new() { GameStateId.Login };
        public override GameStateId Id => GameStateId.InfoLoad;

        public override void StartContext(StateParam stateParam)
        {
            var infoLayer = Game.Current.GetInfo();
            
            RegisterInfos(infoLayer);

            var infoLoadTasks = infoLayer.LoadInfoAsync();
            Task.WhenAll(infoLoadTasks).ContinueWith(t =>
            {
                StateMachine.TryGoToState(GameStateId.Login, new LoginState.Param()
                {
                    Platform = LoginPlatform.Local,
                    Token = new LoginToken()
                });
            }, Game.TaskToken, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext())
            .LogIfFaulted();
        }

        private void RegisterInfos(InfoLayer info)
        {
            info.RegisterInfo(new InventoryInfoManager(CreateLoader<InventoryInfo>()));
            info.RegisterInfo(new GameSettingInfoManager(CreateLoader<GameSettingInfo>()));
        }
        
        private IValueLoader<TInfo> CreateLoader<TInfo>()
        {
            return new SerializationLoader<TInfo>(InfoUtil.CreateStorage(typeof(TInfo)), JsonSerializer.Instance);
        }
    }
}
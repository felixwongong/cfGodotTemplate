using System;
using System.Collections.Generic;
using cfEngine.Core;
using cfEngine.Logging;
using cfEngine.Service;
using cfEngine.Util;

namespace cfGodotTemplate.Core
{
    public class UserDataLoadState : GameState
    {
        public override HashSet<GameStateId> Whitelist { get; } = new() { GameStateId.Initialization };

        public override GameStateId Id => GameStateId.UserDataLoad;

        private void RegisterModels()
        {
            var userDataManager = Game.Current.GetUserData();
            
            foreach (var service in Game.Current)
            {
                if(service is IModelService modelService)
                {
                    userDataManager.Register(modelService.GetModel);
                }
            }
        }

        public override void StartContext(StateParam stateParam)
        {
            RegisterModels();
            
            var userData = Game.Current.GetUserData();
            userData.LoadDataMap().ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    try
                    {
                        userData.InitializeSavables();
                        
                        StateMachine.TryGoToState(GameStateId.Initialization);
                    }
                    catch (Exception e)
                    {
                        Log.LogException(e);
                    }
                }
                else
                {
                    Log.LogException(t.Exception);
                }
            });
        }
    }
}
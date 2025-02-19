using UnityEngine;
using WhoIsBigger.Scripts.Model;
using WhoIsBigger.Scripts.View;
using Zenject;

namespace WhoIsBigger.Scripts.Presenter
{
    public class GamePresenter
    {
        private readonly IGameModel _gameModel;
        private GameUIManager _gameUIManager;
        private EventManager _eventManager;
        
        [Inject]
        public GamePresenter(IGameModel gameModel, GameUIManager gameUIManager, EventManager eventManager)
        {
            _gameModel = gameModel;
            _gameUIManager = gameUIManager;
            
            _eventManager = eventManager;
            _eventManager.OnUnitSpawned.AddListener(OnUnitSpawned);
        }

        private void OnUnitSpawned()
        {
            _gameModel.FriendlyUnitsCount++;
            Debug.Log("OnUnitSpawned");
            _gameUIManager.UpdateStatistics(_gameModel);
        }
    }
}
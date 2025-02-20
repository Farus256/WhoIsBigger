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
            _eventManager.OnUnitDied.AddListener(OnUnitDied);
        }
        
        private void OnUnitSpawned(CapsuleType type)
        {
            switch (type)
            {
                case CapsuleType.Friendly:
                    _gameModel.FriendlyUnitsCount++;
                    break;
                
                case CapsuleType.Enemy:
                    _gameModel.EnemyUnitsCount++;
                    break;
            }
            
            Debug.Log("UnitSpawned");
            _gameUIManager.UpdateStatistics(_gameModel);
        }
        
        private void OnUnitDied(CapsuleType type)
        {
            switch (type)
            {
                case CapsuleType.Friendly:
                    _gameModel.FriendlyUnitsDead++;
                    _gameModel.FriendlyUnitsCount--;
                    break;
                
                case CapsuleType.Enemy:
                    _gameModel.EnemyUnitsDead++;
                    _gameModel.EnemyUnitsCount--;
                    break;
            }
            
            Debug.Log("UnitDied");
            _gameUIManager.UpdateStatistics(_gameModel);
        }
    }
}
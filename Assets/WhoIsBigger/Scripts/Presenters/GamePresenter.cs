using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;
using Zenject;

namespace WhoIsBigger.Scripts.Presenters
{
    public class GamePresenter
    {
        [Inject] private CapsuleFactory _capsuleFactory;
        
        private readonly IGameModel _gameModel;
        private GameUIManager _gameUIManager;
        private EventManager _eventManager;
        
        [Inject]
        public GamePresenter(IGameModel gameModel, GameUIManager gameUIManager, EventManager eventManager)
        {
            _gameModel = gameModel;
            _gameUIManager = gameUIManager;
            
            _eventManager = eventManager;
            _eventManager.OnUnitSpawn.AddListener(OnUnitSpawned);
            _eventManager.OnUnitDie.AddListener(OnUnitDied);
        }
        
        private void OnUnitSpawned(CapsuleType capsuleType, Vector3 pos)
        {
            switch (capsuleType)
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
        
        private void OnUnitDied(CapsuleController capsuleController)
        {
            CapsuleType type = capsuleController.CapsuleType;
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
                
                default:
                    Debug.LogError("Unknown capsule type");
                    break;
            }
            
            Debug.Log("UnitDied");
            _gameUIManager.UpdateStatistics(_gameModel);
        }
    }
}
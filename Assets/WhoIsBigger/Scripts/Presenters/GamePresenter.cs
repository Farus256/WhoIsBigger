using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Models;
using WhoIsBigger.Scripts.Services.SpawnService;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;
using Zenject;

public class GamePresenter
{
    private readonly IGameModel _gameModel;
    private readonly IGameUI _gameUI;
    private readonly EventManager _eventManager;
    private readonly ISpawnService _spawnService;

    [Inject]
    public GamePresenter(
        IGameModel gameModel,
        IGameUI gameUI,
        EventManager eventManager,  
        ISpawnService spawnService)
    {
        _gameModel = gameModel;
        _gameUI = gameUI;
        _eventManager = eventManager;
        _spawnService = spawnService;
        
        _eventManager.OnUnitSpawn.AddListener(OnUnitSpawned);
        _eventManager.OnUnitDie.AddListener(OnUnitDied);
    }

    private void OnUnitSpawned(EntityType entityType, Vector3 pos)
    {
        // Спавн
        _spawnService.SpawnCapsule(entityType, pos);

        // Обновляем статистику
        switch (entityType)
        {
            case EntityType.Friendly:
                _gameModel.FriendlyUnitsCount++;
                break;
            case EntityType.Enemy:
                _gameModel.EnemyUnitsCount++;
                break;
            default:
                Debug.Log("Unknown capsule type");
                return;
        }

        _gameUI.UpdateStatistics(_gameModel);
    }

    private void OnUnitDied(CapsuleController capsuleController)
    {
        // Обновляем статистику
        switch (capsuleController.EntityType)
        {
            case EntityType.Friendly:
                _gameModel.FriendlyUnitsDead++;
                _gameModel.FriendlyUnitsCount--;
                break;
            case EntityType.Enemy:
                _gameModel.EnemyUnitsDead++;
                _gameModel.EnemyUnitsCount--;
                break;
            default:
                Debug.Log("Unknown capsule type");
                return;
        }
        
        _gameUI.UpdateStatistics(_gameModel);
        
        // Удаляем
        _spawnService.DestroyCapsule(capsuleController);
    }
}

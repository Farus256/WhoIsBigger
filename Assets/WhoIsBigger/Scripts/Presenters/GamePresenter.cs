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
        _eventManager.OnCapsuleCollide.AddListener(OnCapsuleCollided);
    }

    private void OnUnitSpawned(CapsuleType capsuleType, Vector3 pos)
    {
        // Спавн
        _spawnService.SpawnCapsule(capsuleType, pos);

        // Обновляем статистику
        switch (capsuleType)
        {
            case CapsuleType.Friendly:
                _gameModel.FriendlyUnitsCount++;
                break;
            case CapsuleType.Enemy:
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
        switch (capsuleController.CapsuleType)
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
                Debug.Log("Unknown capsule type");
                return;
        }
        
        _gameUI.UpdateStatistics(_gameModel);
        
        // Удаляем
        _spawnService.DestroyCapsule(capsuleController);
    }

    private void OnCapsuleCollided(CapsuleController c1, CapsuleController c2)
    {
        _eventManager.OnUnitDie.Invoke(c1);
        _eventManager.OnUnitDie.Invoke(c2);
    }
}

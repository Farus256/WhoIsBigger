using UnityEngine;
using WhoIsBigger.Scripts.Common;
using WhoIsBigger.Scripts.Services;
using WhoIsBigger.Scripts.Services.CapsuleService;
using WhoIsBigger.Scripts.Services.SpawnService;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.Capsule;
using Zenject;

namespace WhoIsBigger.Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform capsulesContainer;
        [SerializeField] private GameObject friendlyCapsulePrefab;
        [SerializeField] private GameObject enemyCapsulePrefab;

        public override void InstallBindings()
        {
            // DI
            Container.Bind<IRegisterService>().To<RegisterService>().AsSingle();
            Container.Bind<ISpawnService>().To<SpawnService>().AsSingle();
            Container.Bind<EventManager>().AsSingle();
            
            // Контейнер для капсул
            Container.Bind<Transform>().WithId("CapsulesContainer").FromInstance(capsulesContainer).AsSingle();

            // Фабрика
            Container.BindInstance(friendlyCapsulePrefab).WithId("FriendlyCapsulePrefab").AsTransient();
            Container.BindInstance(enemyCapsulePrefab).WithId("EnemyCapsulePrefab").AsTransient();
            Container.BindFactory<EntityType, Vector3, CapsuleController, CapsuleFactory>().AsSingle();

            // Presenter
            Container.Bind<GamePresenter>().AsSingle().NonLazy();

            // Model
            Container.Bind<IGameUI>().To<GameUIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ClickController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitSpawner>().FromComponentInHierarchy().AsSingle();
        }
    }
}
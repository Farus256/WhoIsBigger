using UnityEngine;
using WhoIsBigger.Scripts.Presenter;
using WhoIsBigger.Scripts.View;
using Zenject;

namespace WhoIsBigger.Scripts.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject friendlyCapsulePrefab;
        [SerializeField] private GameObject enemyCapsulePrefab;

        public override void InstallBindings()
        { 
            Container.Bind<GamePresenter>().AsSingle().NonLazy();
            
            Container.Bind<GameUIManager>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<ClickController>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EventManager>().FromComponentInHierarchy().AsSingle();

            Container.BindInstance(friendlyCapsulePrefab).WithId("FriendlyCapsulePrefab").AsTransient();
            Container.BindInstance(enemyCapsulePrefab).WithId("EnemyCapsulePrefab").AsTransient();

            Container.BindFactory<CapsuleType, Vector3, CapsuleController, CapsuleFactory>().AsSingle();

            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();

            //Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();

            //Container.Bind<GameObject>().WithId("EnemyCapsulePrefab").FromInstance(enemyCapsulePrefab);
        }
    }
}
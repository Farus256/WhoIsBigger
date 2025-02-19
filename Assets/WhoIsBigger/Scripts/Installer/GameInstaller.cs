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
            
            Container.BindFactory<Vector3, FriendlyCapsuleController, FriendlyCapsuleFactory>()
                .FromComponentInNewPrefab(friendlyCapsulePrefab)
                .UnderTransformGroup("FriendlyCapsule");
            
            //Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
            
            //Container.Bind<GameObject>().WithId("EnemyCapsulePrefab").FromInstance(enemyCapsulePrefab);
            
        }
    }
}
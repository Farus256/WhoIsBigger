using UnityEngine;
using WhoIsBigger.Scripts.Presenter;
using WhoIsBigger.Scripts.View;
using Zenject;

namespace WhoIsBigger.Scripts.Installer
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        { 
            Container.Bind<GamePresenter>().AsSingle().NonLazy();
            
            Container.Bind<GameUIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ClickController>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EventManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
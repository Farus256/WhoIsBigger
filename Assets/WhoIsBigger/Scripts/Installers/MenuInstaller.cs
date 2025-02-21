using UnityEngine.SceneManagement;
using WhoIsBigger.Scripts.Views;
using WhoIsBigger.Scripts.Views.UIManagers;
using Zenject;

namespace WhoIsBigger.Scripts.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuSceneManager>().AsSingle();
            Container.Bind<MenuUIManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
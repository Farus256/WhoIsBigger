using WhoIsBigger.Scripts.Models;
using Zenject;

namespace WhoIsBigger.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        }    
    }
}

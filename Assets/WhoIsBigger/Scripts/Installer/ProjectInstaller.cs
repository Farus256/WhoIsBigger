using WhoIsBigger.Scripts.Model;
using Zenject;

namespace WhoIsBigger.Scripts.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<IGameModel>().To<GameModel>().AsSingle();
        }    
    }
}

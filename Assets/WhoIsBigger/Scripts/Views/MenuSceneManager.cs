using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WhoIsBigger.Scripts.Views
{
    public class MenuSceneManager
    {
        public async UniTask LoadGameSceneAsync(string sceneName)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }
        }
    }
}
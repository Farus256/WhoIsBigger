using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WhoIsBigger.Scripts.Models;
using Zenject;

namespace WhoIsBigger.Scripts.Views.UIManagers
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text enemyCountText;
        [SerializeField] private string gameSceneName = "GameScene";
        
        private IGameModel _gameModel;
        private MenuSceneManager _menuMenuSceneManager;

        [Inject]
        public void Construct(IGameModel gameModel, MenuSceneManager menuMenuSceneManager)
        {
            _gameModel = gameModel;
            _menuMenuSceneManager = menuMenuSceneManager;
        }

        public void Start()
        {
            enemyCountText.text = "Max Units: " + _gameModel.MaxUnitsCount;
        }

        public void OnStartButtonClicked()
        {
            LoadGameSceneAsync().Forget();
        }
        
        public void OnAddButtonClicked(int value)
        {
            _gameModel.MaxUnitsCount += value;
            enemyCountText.text = "Max Units: " + _gameModel.MaxUnitsCount;
        }
        
        public void OnMinusButtonClicked(int value)
        {
            _gameModel.MaxUnitsCount -= value;
            enemyCountText.text = "Max Units: " + _gameModel.MaxUnitsCount;
            
        }
    
        private async UniTask LoadGameSceneAsync()
        {
            await _menuMenuSceneManager.LoadGameSceneAsync(gameSceneName);
        }
    }
}
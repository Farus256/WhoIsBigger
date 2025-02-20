using TMPro;
using UnityEngine;
using WhoIsBigger.Scripts.Models;

namespace WhoIsBigger.Scripts.Views
{
    public class GameUIManager : MonoBehaviour
    {
        public TMP_Text statisticsText;
        
        public void UpdateStatistics(IGameModel gameModel)
        {
            Debug.Log(statisticsText);
            statisticsText.text = $"Friendly: {gameModel.FriendlyUnitsCount}" +
                                  $"\nEnemies: {gameModel.EnemyUnitsCount}" +
                                  $"\nDead Friendlies: {gameModel.FriendlyUnitsDead}" +
                                  $"\nDead Enemies: {gameModel.EnemyUnitsDead}";
        }
    }
}
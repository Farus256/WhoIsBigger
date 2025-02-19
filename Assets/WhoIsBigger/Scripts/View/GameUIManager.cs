using System;
using TMPro;
using UnityEngine;
using WhoIsBigger.Scripts.Model;

namespace WhoIsBigger.Scripts.View
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
                                  $"\nDead Enemies boba: {gameModel.EnemyUnitsDead}";
        }
    }
}
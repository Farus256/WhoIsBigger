namespace WhoIsBigger.Scripts.Models
{
    public class GameModel : IGameModel
    {
        public int FriendlyUnitsCount{get;set;}
        public int EnemyUnitsCount{get;set;}
        public int FriendlyUnitsDead{get;set;}
        public int EnemyUnitsDead{get;set;}
    }
}
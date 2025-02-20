namespace WhoIsBigger.Scripts.Models
{
    public interface IGameModel
    {
        int FriendlyUnitsCount{get;set;}
        int EnemyUnitsCount{get;set;}
        int FriendlyUnitsDead{get;set;}
        int EnemyUnitsDead{get;set;}
    }
}
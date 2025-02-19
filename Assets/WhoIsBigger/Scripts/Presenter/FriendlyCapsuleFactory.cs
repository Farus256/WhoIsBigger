using UnityEngine;
using WhoIsBigger.Scripts.View;
using Zenject;

namespace WhoIsBigger.Scripts.Presenter
{
    public class FriendlyCapsuleFactory : PlaceholderFactory<Vector3, FriendlyCapsuleController>
    {
        //обёртка, основа находится в Zenject
    }
}
using UnityEngine;
using View.Background;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private BackgroundCreator _backgroundCreator;

    public override void InstallBindings() //тут делаются регистрации
    {
       
    }

    
}

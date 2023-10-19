using UnityEngine;
using View.Background;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private StructureBG _structureBg;
    public override void InstallBindings()
    {
        BindBackground();
    }
    private void BindBackground()
    {
        Container.Bind<StructureBG>().FromInstance(_structureBg);
        //BackgroundCreator background = Container.InstantiatePrefabForComponent<BackgroundCreator>(_backgroundCreator);
    }
}

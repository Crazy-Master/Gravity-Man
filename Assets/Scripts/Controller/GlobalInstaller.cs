using Core.AudioSystem;
using Core.WindowSystem;
using UnityEngine;
using View.Background;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private StructureBG _structureBg;
    [SerializeField] private WindowDataBase _windowDataBase;
    [SerializeField] private AudioDataBase _audioDataBase;
    public override void InstallBindings()
    {
        BindBackground();
        BindWindow();
    }
    private void BindBackground()
    {
        Container.Bind<StructureBG>().FromInstance(_structureBg);
        //BackgroundCreator background = Container.InstantiatePrefabForComponent<BackgroundCreator>(_backgroundCreator);
    }
    private void BindWindow()
    {
        Container.Bind<WindowDataBase>().FromInstance(_windowDataBase);
        //BackgroundCreator background = Container.InstantiatePrefabForComponent<BackgroundCreator>(_backgroundCreator);
    }
}

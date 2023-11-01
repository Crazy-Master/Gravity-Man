using Core.AudioSystem;
using Core.WindowSystem;
using UnityEngine;
using View.Background;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private StructureBG _structureBg;
    [SerializeField] private StructureWindow _structureWindow;
    [SerializeField] private StructureLoadLevel _structureLoadLevel;
    [SerializeField] private AudioDataBase _audioDataBase;
    public override void InstallBindings()
    {
        BindBackground();
        BindWindow();
        BindLevel();
    }
    private void BindBackground()
    {
        Container.Bind<StructureBG>().FromInstance(_structureBg);
        //BackgroundCreator background = Container.InstantiatePrefabForComponent<BackgroundCreator>(_backgroundCreator);
    }
    private void BindWindow()
    {
        Container.Bind<StructureWindow>().FromInstance(_structureWindow);
    }
    
    private void BindLevel()
    {
        Container.Bind<StructureLoadLevel>().FromInstance(_structureLoadLevel);
    }
}

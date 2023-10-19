using System;
using System.Collections.Generic;
using UnityEngine;

namespace View.Background
{
    public enum EScene
    {
        MenuScene,
        GameScene1,
        GameScene2,
        GameScene3
    }

    [System.Serializable]
    public struct BackgroundLayer
    {
        public Sprite sprite;
        public float modifier;
    }

    [Serializable]
    public struct BackgroundScene
    {
        public EScene scene;
        public BackgroundLayer[] layers;
        
        /*[Space]
        [Header("Structure size")]*/
    }

    [CreateAssetMenu(fileName = "StructureDataBase", menuName = "Gameplay/New StructureBG")]
    public class StructureBG : ScriptableObject
    {
        [SerializeField] List<BackgroundScene> structureSettings;
    
        public List<BackgroundScene> SettingStructure => structureSettings;

        
        public BackgroundLayer[] GetBackgroundLayer(EScene structure)
        {
            return GetStructureSetting(structure).layers;
        }
        
        

        private BackgroundScene GetStructureSetting(EScene scene)
        {
            for (int i = 0; i < structureSettings.Count; i++)
            {
                if (SettingStructure[i].scene == scene)
                {
                    return SettingStructure[i];
                }
            }
            return SettingStructure[0];
        }
    }
}

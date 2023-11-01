using System;
using System.Collections.Generic;
using UnityEngine;

 
    [Serializable]
    public struct SettingLevel
    {
        public GameObject grid;
        public GameObject decor;
        public Vector3 posPlayer;
    }

[CreateAssetMenu(fileName = "LevelDataBase", menuName = "Settings/LevelDataBase", order = 0)]

public class StructureLoadLevel : ScriptableObject
{
    [SerializeField] private List<SettingLevel> _levelSettings;
     
    public SettingLevel GetLevel(int level)
    {
        return _levelSettings[level - 1];
    }
}

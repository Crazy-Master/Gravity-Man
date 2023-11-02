using System;
using System.Collections.Generic;
using UnityEngine;

 
    [Serializable]
    public struct SettingLevel
    {
        public GameObject grid;
        public GameObject decor;
        public float gravity;
        [Space] [Header("Player")] 
        public Vector3 posPlayer;
        public float speed;
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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.WindowSystem
{
 public enum EWindow
 { 
     MineManu,
     LevelsManu,
     LoadingManu,
     GameManu,
     PauseMenu,
     SettingManu,
     ADMenu,
     LevelComplete,
     GameOver
 }
 
[Serializable]
 public class SettingWindow
 {
     public EWindow eWindow;
     public GameObject windowObject;
 }
 
 [CreateAssetMenu(fileName = "WindowDataBase", menuName = "Settings/WindowDataBase", order = 0)]
 public class WindowDataBase : ScriptableObject
 {
     [SerializeField] private List<SettingWindow> _windowSettings;
     
     public GameObject GetWindow(EWindow structure)
     {
         return GetStructureSetting(structure).windowObject;
     }
        
        

     public SettingWindow GetStructureSetting(EWindow structure)
     {
         for (int i = 0; i < _windowSettings.Count; i++)
         {
             if (_windowSettings[i].eWindow == structure)
             {
                 return _windowSettings[i];
             }
         }
         return _windowSettings[0];
     }
 }   
}


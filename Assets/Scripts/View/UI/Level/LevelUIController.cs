using System;
using UnityEngine;
using YG;

namespace Core.Level
{
    [Serializable]
    public struct LevelData
   {
      public int level;
      public bool locked;
      public int stars;

      public LevelData(int lvl, bool loc, int star)
      {
          level = lvl;
          locked = loc;
          stars = star;
      }
   }
   public class LevelUIController : MonoBehaviour
   {
       [SerializeField] private Transform _transformParent;
       [SerializeField] private GameObject _prefab;
       [SerializeField] private WindowController _windowController;
       [SerializeField] private ButtonLevelController[] Level;
       
       [SerializeField] private StructureLoadLevel _structureLoadLevel;
       
       [SerializeField] private LevelData[] _levelDatas; //заменить на SavesYG
       
       private void Start()
       {
           var levelDatas = YandexGame.savesData.LevelDatas;
           if (levelDatas == null) return;
           for (int i = 0; i < _levelDatas.Length; i++)
           {
               Level[i].Init(levelDatas[i],_windowController);
           }
       }

       [ContextMenu("SetSaveLevel")]

       public void SetSaveLevel()
       {
           #if UNITY_EDITOR
           if (Level != null)
               for (int i = 0; i < Level.Length; i++)
               {
                   DestroyImmediate(Level[i].gameObject);
               }
           
           Level = new ButtonLevelController[_structureLoadLevel.GetQuantityLevel()];
           for (int i = 0; i < _structureLoadLevel.GetQuantityLevel(); i++)
           {
               Level[i] = Instantiate(_prefab, _transformParent).GetComponent<ButtonLevelController>();
           }
           UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
           
           #endif
       }
   } 
}


using System;
using UnityEngine;
using YG;

namespace Core.Level
{
    [Serializable]
    public struct LevelData
   {
      public bool openLevel;
      public int stars;

      public LevelData(bool open, int star)
      {
          openLevel = open;
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

       private void Start()
       {
           var levelDatas = YandexGame.savesData.LevelDatas;
           if (levelDatas == null) return;
           for (int i = 0; i < levelDatas.Length; i++)
           {
               Level[i].Init(i, levelDatas[i],_windowController);
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
           YandexGame.savesData.SetQuantityLevel(_structureLoadLevel.GetQuantityLevel());
           UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
           
#endif
       }
   } 
}


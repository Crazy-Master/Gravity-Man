using System;
using UnityEngine;
using UnityEngine.UI;
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

       [SerializeField] private Button _buttonStartLastLevel;
       private int _lastLevel;
       private void Start()
       {
           var levelDatas = YandexGame.savesData.LevelDatas;
           if (levelDatas == null) return;
           for (int i = 0; i < levelDatas.Length; i++)
           {
               Level[i].Init(i, levelDatas[i],_windowController);
               
               if (levelDatas[i].openLevel == true)
               {
                   _lastLevel = i;
               }
           }

           _buttonStartLastLevel.onClick.AddListener(()=>_windowController.LoadSingleLevel(_lastLevel));
       }

       [ContextMenu("SetSaveLevel")]

       public void SetSaveLevel()
       {
#if UNITY_EDITOR
           if (Level != null)
               for (int i = 0; i < Level.Length; i++)
               {
                   if (Level[i] != null) DestroyImmediate(Level[i].gameObject);
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


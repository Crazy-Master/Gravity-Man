using System;
using UnityEngine;

namespace Core.Level
{
    [Serializable]
    public struct LevelData
   {
      public int level;
      public bool locked;
      public int star;
   }
   public class LevelManager : MonoBehaviour
   {
       [SerializeField] private Transform _transformParent;
       [SerializeField] private GameObject _prefab;
       [SerializeField] private WindowController _windowController;

       [SerializeField] private LevelData[] _levelDatas;
       public void Init(int QuantityLevel)
       {
           for (int i = 0; i < QuantityLevel; i++)
           {
               var level = Instantiate(_prefab, _transformParent);
               level.GetComponent<LevelUI>().Init(_levelDatas[i], _windowController);
           }
           
       }
   } 
}


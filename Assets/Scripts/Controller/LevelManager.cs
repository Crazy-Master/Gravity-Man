using System.Collections.Generic;
using Core.Level;
using UnityEngine;
using YG;

public class LevelManager
{
    private WorldController _worldController;

    public LevelManager(WorldController worldController)
    {
        _worldController = worldController;
        _worldController.OnLevelComplete += LevelComplete;
    }

    public void InitLevels()
    {
        if (YandexGame.savesData.LevelDatas == null)
        {
            var levelDatas = new List<LevelData>();
            levelDatas.Add(new LevelData(1, false, 0));
            YandexGame.savesData.LevelDatas = levelDatas;
        }
    }
    
    private void LevelComplete(int star)
    {
        var level = _worldController.ActiveLevel;
        var levelDatas = YandexGame.savesData.LevelDatas[level - 1];
        if (star > levelDatas.stars)
        {
            YandexGame.savesData.LevelDatas[level-1] = new LevelData(level, true, star);
        }
    }
}

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

    private void LevelComplete(int star)
    {
        var level = _worldController.ActiveLevel;
        if (star > YandexGame.savesData.LevelDatas[level].stars)
        {
            YandexGame.savesData.LevelDatas[level] = new LevelData(true, star);
        }

        YandexGame.savesData.LevelDatas[level+1].openLevel = true;
        YandexGame.SaveProgress();
    }
}

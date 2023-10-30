
using Core.AudioSystem;
using Core.WindowSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WindowDataBase WindowDataBase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else //if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}

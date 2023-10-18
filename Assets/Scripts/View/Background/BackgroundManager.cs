using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    //[SerializeField] private BackgroundLayer[] _layers; переделать
    [SerializeField] private Sprite[] _sprite;
    [SerializeField] private float[] _modifier;
    [SerializeField] private GameObject _prefabLayer;

    private void Start()
    {
        CreateLayersBackground();
        
    }

    private void CreateLayersBackground()
    {
        Transform transform = gameObject.transform;
        for (int i = 0; i < _sprite.Length; i++)
        {
            var bject = Instantiate(_prefabLayer, transform);
            var comp = bject.GetComponent<EndlessScrolling>();
            comp.Init(GetScaleSprite(), _sprite[i], _modifier[i],i);
            
        }
    }

    private Vector3 GetScaleSprite()
    {
        if (Camera.main is null) return new Vector3(0,0,0);
    
        var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = topRightCorner.x * 2;
        var worldSpaceHeight = topRightCorner.y * 2;

        var spriteSize = _sprite[0].bounds.size;

        var scaleFactorX = worldSpaceWidth / spriteSize.x;
        var scaleFactorY = worldSpaceHeight / spriteSize.y;

        if (scaleFactorX > scaleFactorY)
        {
            scaleFactorY = scaleFactorX;
        }
        else
        {
            scaleFactorX = scaleFactorY;
        }

        return new Vector3(scaleFactorX, scaleFactorY, 1);
    }
}



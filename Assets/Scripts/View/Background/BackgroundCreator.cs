using System.Collections.Generic;
using UnityEngine;
using View.Background;
using Zenject;

[System.Serializable]

public class BackgroundCreator : MonoBehaviour
{
    [SerializeField] private EScene _scene;
    private StructureBG _structureBg;
    private const int NumberSprites = 3; 
    private Vector3 _topRightCorner;
    private List<GameObject> _layers = new List<GameObject>();
    
    [Inject]
    private void Construct(StructureBG structure) => _structureBg = structure;

    private void Start()
    {
        CreateLayersBackground(_scene);
    }

    public void Restart(EScene eScene)
    {
        for (int i = 0; i < _layers.Count; i++)
        {
            Destroy(_layers[i]);
        }
        _layers.Clear();
        CreateLayersBackground(eScene);
    }

    private void CreateLayersBackground(EScene eScene)
    {
        if (Camera.main is not null) _topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        var BackgroundLayer = _structureBg.GetBackgroundLayer(eScene);
        for (int i = 0; i < BackgroundLayer.Length; i++)
        {
            var comp = CreateLayer(transform);
            comp.Init(GetScaleSprite(BackgroundLayer[i].sprite), BackgroundLayer[i].sprite, BackgroundLayer[i].modifier,i);
            
        }
    }

    private EndlessScrolling CreateLayer(Transform valu)
    {
        var LayerBG = new GameObject("LayerBG");
        _layers.Add(LayerBG);
        LayerBG.transform.SetParent(valu);
        var comp = LayerBG.AddComponent<EndlessScrolling>();
        SpriteRenderer[] spriteRenderers = new SpriteRenderer[NumberSprites];
        for (int i = 0; i < NumberSprites; i++)
        {
            var objScript = new GameObject("Sprite " + i);
            spriteRenderers[i] = objScript.AddComponent<SpriteRenderer>();
            objScript.transform.SetParent(LayerBG.transform);
        }
        comp.SetSpriteRenderer(spriteRenderers);
        return comp;
    }

    private Vector3 GetScaleSprite(Sprite sprite)
    {
        var worldSpaceWidth = _topRightCorner.x * 2;
        var worldSpaceHeight = _topRightCorner.y * 2;

        var spriteSize = sprite.bounds.size;

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



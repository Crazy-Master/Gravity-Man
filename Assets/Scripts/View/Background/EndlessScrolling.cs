using System.Collections.Generic;
using UnityEngine;

public class EndlessScrolling : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _arraySprite = new SpriteRenderer[2];
    private GameObject[] _arrayObjSprite = new GameObject[2];
    private Queue<GameObject> _queueSprite = new Queue<GameObject>();
    private Vector3[] _startPosition = new Vector3[2];
    private Camera _cam;
    private float _modifier;
    private float _width;

    private bool first = true;
    public void Init(Vector3 scale, Sprite sprite, float modifier, int orderInLayer)
    {
        _cam = Camera.main;
        _modifier = modifier;
        for (int i = 0; i < _arraySprite.Length; i++)
        {
            var cell = _arraySprite[i];
            cell.transform.localScale = scale;
            if (_width == 0) _width = sprite.bounds.size.x * scale.x;
            cell.sprite = sprite;
            var position = cell.transform.position;
            position += new Vector3(_width * i, 0, 0);
            cell.transform.position = position;
            _startPosition[i] = position;
            cell.sortingOrder = orderInLayer;
            
            _queueSprite.Enqueue(cell.gameObject);
            _arrayObjSprite[i] = cell.gameObject;
        }
    }
    
    private void Update()
    {
        MoveSprites();
        if ((_cam.transform.position.x - _queueSprite.Peek().transform.position.x) > _width)
        {
            TransferSprite();
        }
        /*if ((cam.transform.position.x - transform.position.x) < -width)
        {
            startPosition -= new Vector3(width, 0, 0);
        }*/ //назад идти не может
    }

    private void MoveSprites()
    {
        Vector3 camOffset = _cam.transform.position * _modifier; 
        for (int i = 0; i < _arrayObjSprite.Length; i++)
        {
            _arrayObjSprite[i].transform.position = _startPosition[i] + new Vector3(camOffset.x, camOffset.y, 0); 
        }
    }

    private void TransferSprite()
    {
        var sprite = _queueSprite.Dequeue();
        _queueSprite.Enqueue(sprite);
        if (first)
        {
            _startPosition[0] += new Vector3(_width * 2, 0, 0);  
        }
        else
        {
            _startPosition[1] += new Vector3(_width * 2, 0, 0);  
        }
        first = !first;
    }
    
}

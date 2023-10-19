using System.Collections.Generic;
using UnityEngine;



public class EndlessScrolling : MonoBehaviour
{
    private SpriteRenderer[] _arraySprite;
    private GameObject[] _arrayObjSprite;
    private Vector3[] _startPosition;
    private int _length;
    private int _centre;
    
    private Camera _cam;
    private float _modifier;
    private float _width;

    private bool first = true;

    public void SetSpriteRenderer(SpriteRenderer[] spriteRenderers)
    {
        if (_arraySprite == null)
        {
            _arraySprite = spriteRenderers;
            _length = _arraySprite.Length;
            _centre = (int)((_length - 1) / 2);
            _arrayObjSprite = new GameObject[_length];
            _startPosition = new Vector3[_length];
        }
    }
    public void Init(Vector3 scale, Sprite sprite, float modifier, int orderInLayer)
    {
        _cam = Camera.main;
        _modifier = modifier;
        for (int i = 0; i < _length; i++)
        {
            var cell = _arraySprite[i];
            cell.transform.localScale = scale;
            if (_width == 0) _width = sprite.bounds.size.x * scale.x;
            cell.sprite = sprite;
            
            var cellTransform = cell.transform;
            var position = cellTransform.position;
            position += new Vector3(_width * (i-_centre), 0, 0);
            cellTransform.position = position;
            _startPosition[i] = position;
            cell.sortingOrder = orderInLayer;
            _arrayObjSprite[i] = cell.gameObject;
        }
    }
    
    private void Update()
    {
        MoveSprites();
        if ((_cam.transform.position.x - _arrayObjSprite[_centre].transform.position.x) > _width)
        {
            TransferRight();
        }
        if ((_cam.transform.position.x - _arrayObjSprite[_centre].transform.position.x) < -_width)
        {
            TransferLeft();
        }
    }

    private void MoveSprites()
    {
        Vector3 camOffset = _cam.transform.position * _modifier; 
        for (int i = 0; i < _length; i++)
        {
            _arrayObjSprite[i].transform.position = _startPosition[i] + new Vector3(camOffset.x, camOffset.y, 0); 
        }
    }

    private void TransferRight()
    {
        _startPosition[0] += new Vector3(_width * _length, 0, 0);
        GameObject obj = _arrayObjSprite[0];
        Vector3 vec = _startPosition[0];
        for (int i = 0; i < _length-1; i++)
        {
            _arrayObjSprite[i] = _arrayObjSprite[i + 1];
            _startPosition[i] = _startPosition[i + 1];
        }
        _arrayObjSprite[_length-1] = obj;
        _startPosition[_length-1] = vec;
    }
    
    private void TransferLeft()
    {
        
        _startPosition[_length-1] -= new Vector3(_width * _length, 0, 0);
        GameObject obj = _arrayObjSprite[_length-1];
        Vector3 vec = _startPosition[_length-1];
        for (int i = _length - 1; i > 0; i--)
        {
            _arrayObjSprite[i] = _arrayObjSprite[i - 1];
            _startPosition[i] = _startPosition[i - 1];
        }
        _arrayObjSprite[0] = obj;
        _startPosition[0] = vec;
    }
    
}

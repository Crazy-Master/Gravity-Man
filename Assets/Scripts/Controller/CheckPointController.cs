using Player;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    
    [SerializeField] private Vector3 _position;
    [SerializeField] private int _gravity;
    public GameObject point;
    
    [ContextMenu("SetSavePlayer")]
    private void SetSavePlayer()
    {
        #if UNITY_EDITOR
        
        
        if (point.transform.rotation.x == 0)
        {
            _position = point.transform.position + new Vector3(0,0.1f,0);
            _gravity = 1;
        }
        else
        {
            _position = point.transform.position - new Vector3(0,0.1f,0);
            _gravity = -1;
        }
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        #endif
    }

    public Vector3 GetPosition()
    {
        return _position;
    }
    public int GetGravity()
    {
        return _gravity;
    }
}

using UnityEngine;

[ExecuteInEditMode]
public class AutoAlign : MonoBehaviour
{
    private GridObject _owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _owner = GetComponent<GridObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            return; // 只在编辑模式下执行
        }

        // 获取当前物体的Transform组件
        
        if (_owner != null)
        {
            // 将物体的位置对齐到整数网格
            _owner.transform.position = new Vector3(Mathf.Round(_owner.transform.position.x), Mathf.Round(_owner.transform.position.y), _owner.transform.position.z);
            
        }
    }
}

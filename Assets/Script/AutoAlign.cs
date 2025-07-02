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
            return; // ֻ�ڱ༭ģʽ��ִ��
        }

        // ��ȡ��ǰ�����Transform���
        
        if (_owner != null)
        {
            // �������λ�ö��뵽��������
            _owner.transform.position = new Vector3(Mathf.Round(_owner.transform.position.x), Mathf.Round(_owner.transform.position.y), _owner.transform.position.z);
            
        }
    }
}

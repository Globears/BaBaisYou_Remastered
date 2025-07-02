using UnityEngine;

public class You : Semantic
{

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _owner.Move(_owner.Position + Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _owner.Move(_owner.Position + Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _owner.Move(_owner.Position + Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _owner.Move(_owner.Position + Vector2Int.right);
        }
    }

}

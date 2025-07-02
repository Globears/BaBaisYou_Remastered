using UnityEngine;

public class Push : Semantic
{


    public override void Awake()
    {
        base.Awake();
        GridObject.OnMovingRequest += OnMovingRequest;
    }

    private bool OnMovingRequest(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (_owner == null) return true;
        if (gridObject == _owner) return true;
        if (newPosition != _owner.Position) return true;

        if (_owner.Move(newPosition + newPosition - oldPosition))
        {
            //如果可以沿着移动方向被推动，就允许推动者移动
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        GridObject.OnMovingRequest -= OnMovingRequest;
    }
}

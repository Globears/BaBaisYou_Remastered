using UnityEngine;

public class Push : Semantic
{


    public override void Awake()
    {
        base.Awake();
        GridObject.OnMovingRequest += OnMovingRequest;
        GridObject.OnMovingStart += OnMovingStart;
    }

    private bool OnMovingRequest(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (_owner == null) return true;
        if (gridObject == _owner) return true;
        if (newPosition != _owner.Position) return true;

        if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + newPosition - oldPosition))
        {
            return true;
        }

        return false;
    }

    private void OnMovingStart(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (gridObject == _owner) return;
        if (newPosition != _owner.Position) return;
        _owner.Move(newPosition + newPosition - oldPosition);
    }



    private void OnDestroy()
    {
        GridObject.OnMovingRequest -= OnMovingRequest;
        GridObject.OnMovingStart -= OnMovingStart;
    }
}

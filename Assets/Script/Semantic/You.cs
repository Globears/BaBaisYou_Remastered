using System;
using UnityEngine;

public class You : Semantic
{

    public override void Awake()
    {
        base.Awake();
        GridObject.OnMovingRequest += OnMovingRequest;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + Vector2Int.up))
            {
                _owner.Move(_owner.Position + Vector2Int.up);
            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + Vector2Int.down))
            {
                _owner.Move(_owner.Position + Vector2Int.down);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + Vector2Int.left))
            {
                _owner.Move(_owner.Position + Vector2Int.left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + Vector2Int.right))
            {
                _owner.Move(_owner.Position + Vector2Int.right);
            }
        }
    }

    private bool OnMovingRequest(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (_owner == null) return true;
        if (gridObject == _owner) return true;
        if (newPosition != _owner.Position) return true;

        if (gridObject.WithSemantic(typeof(You)))
        {
            if (_owner.RaiseMovingRequest(_owner, _owner.Position, _owner.Position + newPosition - oldPosition))
            {
                return true;
            }
        }
        return false;
    }

    void OnDestroy()
    {
        GridObject.OnMovingRequest -= OnMovingRequest;
    }

}

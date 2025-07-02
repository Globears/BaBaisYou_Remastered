using TMPro;
using UnityEngine;

public class Stop : Semantic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Awake()
    {
        base.Awake();
        GridObject.OnMovingRequest += OnMovingRequest;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private bool OnMovingRequest(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (_owner == null) return true;
        if (gridObject == _owner) return true;
        // If the new position is the same as the old position, return true to indicate that the request is handled
        if (newPosition != _owner.Position)
        {
            return true;
        }
        // Otherwise, return false to indicate that the request is not handled
        return false;
    }

    private void OnDestroy()
    {
        GridObject.OnMovingRequest -= OnMovingRequest;
    }
}

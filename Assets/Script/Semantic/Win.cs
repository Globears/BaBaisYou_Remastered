using UnityEngine;

public class Win : Semantic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void Awake()
    {
        base.Awake();
        GridObject.OnMovingEnd += OnMovingEnd;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMovingEnd(GridObject gridObject, Vector2Int StartPosition, Vector2Int EndPosition)
    {
        if (gridObject.WithSemantic(typeof(You)))
        {
            Debug.Log("You Win!!!");
        }
    }

    void OnDestroy()
    {
        GridObject.OnMovingEnd -= OnMovingEnd;
    }
}

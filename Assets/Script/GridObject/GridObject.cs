using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;


public abstract class GridObject : MonoBehaviour
{
    public Vector2Int Position { get; set; }

    public delegate bool MovingRequestHandler(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition);

    //OnMovingRequest的bool值用于判断此次移动是否可行
    public static event MovingRequestHandler OnMovingRequest;
    public delegate void MovingEventHandler(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition);
    public static event MovingEventHandler OnMovingStart, OnMovingEnd, OnRegisterPosition;

    protected bool RaiseMovingRequest(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (OnMovingRequest != null)
        {
            Delegate[] delegates = OnMovingRequest.GetInvocationList();
            foreach (MovingRequestHandler handler in delegates)
            {
                if (!handler(gridObject, oldPosition, newPosition))
                {
                    return false; //如果有一个处理器返回false，则此次移动请求被拒绝
                }
            }
        }
        Debug.Log($"Moving Request: {gridObject.name} from {oldPosition} to {newPosition}");
        return true; //默认允许移动
    }

    protected void RaiseMovingStart(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        OnMovingStart?.Invoke(gridObject, oldPosition, newPosition);
    }

    protected void RaiseMovingEnd(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        OnMovingEnd?.Invoke(gridObject, oldPosition, newPosition);
    }

    protected void RaiseRegisterPosition(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        OnRegisterPosition?.Invoke(gridObject, oldPosition, newPosition);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public virtual void Awake()
    {
        Semantic_Is.OnSemanticAdd += OnSemanticAdd;
        Semantic_Is.OnSemanticRemove += OnSemanticRemove;
    }
    protected virtual void Start()
    {
        Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        RaiseRegisterPosition(this, Position, Position);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateVisualPosition()
    {
        transform.position = new Vector3(Position.x, Position.y, 0);
    }

    private IEnumerator SmoothMove(Vector2Int StartPosition, Vector2Int EndPosition)
    {
        float duration = 0.1f;
        float elapsedTime = 0;

        Vector3 Start = new Vector3(StartPosition.x, StartPosition.y, 0);
        Vector3 End = new Vector3(EndPosition.x, EndPosition.y, 0);

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(Start, End, elapsedTime / duration);
            yield return null;
        }
    }

    public bool Move(Vector2Int targetPosition)
    {
        if (!RaiseMovingRequest(this, Position, targetPosition)) return false;

        Vector2Int oldPosition = Position;
        Vector2Int newPosition = targetPosition;

        RaiseMovingStart(this, oldPosition, newPosition);
        Position = newPosition;
        RaiseMovingEnd(this, oldPosition, newPosition);

        //UpdateVisualPosition();
        StartCoroutine(SmoothMove(oldPosition, newPosition));
        return true;
    }

    protected virtual void OnSemanticAdd(Type semanticType, Type objectType)
    {
        // Handle the semantic addition logic here
        if (objectType.IsAssignableFrom(this.GetType()) && GetComponent(semanticType) == null)
        {
            // For example, you can log the semantic type added to Baba
            gameObject.AddComponent(semanticType);
        }
    }

    protected virtual void OnSemanticRemove(Type semanticType, Type objectType)
    {
        if (objectType.IsAssignableFrom(this.GetType()) )
        {
            // For example, you can log the semantic type added to Baba
            Component[] components = gameObject.GetComponents(semanticType);


            foreach (Component comp in components)
            {
                Destroy(comp);
            }
        }
    }
}

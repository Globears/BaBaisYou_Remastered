using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Collections;


public struct Pair {
    public Type objectType;
    public Type semanticType;

    public Pair(Type objectType, Type semanticType)
    {
        this.objectType = objectType;
        this.semanticType = semanticType;
    }
}


public class Semantic_Is : MonoBehaviour
{
    private GridObject _owner;

    private static List<Pair> pairedObjects = new List<Pair>();

    public delegate void SemanticChangedEventHandler(Type semanticType, Type objectType);
    public static event SemanticChangedEventHandler OnSemanticAdd, OnSemanticRemove;//当物体需要添加或移除语义时通知
                                                                                    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        _owner = GetComponent<GridObject>();
        GridObject.OnMovingEnd += OnMovingEnd;
        GridObject.OnRegisterPosition += OnRegisterPosition;
    }


    private List<Pair> GetPairsAt(Vector2Int position)
    {
        List<Pair> pairs = new List<Pair>();
        GridObject topObject = Map.Instance.GetGridObject(position + Vector2Int.up);
        GridObject bottomObject = Map.Instance.GetGridObject(position + Vector2Int.down);
        if (topObject != null && bottomObject != null)
        {
            if (topObject.GetComponent<SemanticSymbol>() != null && bottomObject.GetComponent<ObjectSymbol>() != null)
            {
                Type objectType = bottomObject.GetComponent<ObjectSymbol>().refer;
                Type semanticType = topObject.GetComponent<SemanticSymbol>().refer;
                pairs.Add(new Pair(objectType, semanticType));
            }
            else if (bottomObject.GetComponent<SemanticSymbol>() != null && topObject.GetComponent<ObjectSymbol>() != null)
            {
                Type objectType = topObject.GetComponent<ObjectSymbol>().refer;
                Type semanticType = bottomObject.GetComponent<SemanticSymbol>().refer;
                pairs.Add(new Pair(objectType, semanticType));
            }
        }
        GridObject leftObject = Map.Instance.GetGridObject(position + Vector2Int.left);
        GridObject rightObject = Map.Instance.GetGridObject(position + Vector2Int.right);
        if (leftObject != null && rightObject != null)
        {
            if (leftObject.GetComponent<SemanticSymbol>() != null && rightObject.GetComponent<ObjectSymbol>() != null)
            {
                Type objectType = rightObject.GetComponent<ObjectSymbol>().refer;
                Type semanticType = leftObject.GetComponent<SemanticSymbol>().refer;
                pairs.Add(new Pair(objectType, semanticType));
            }
            else if (rightObject.GetComponent<SemanticSymbol>() != null && leftObject.GetComponent<ObjectSymbol>() != null)
            {
                Type objectType = leftObject.GetComponent<ObjectSymbol>().refer;
                Type semanticType = rightObject.GetComponent<SemanticSymbol>().refer;
                pairs.Add(new Pair(objectType, semanticType));
            }
        }

        return pairs;
    }

    private void UpdateSemantic(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        //自己在动
        if (gridObject == _owner)
        {
            //检查原来位置上的pair
            List<Pair> oldPairs = GetPairsAt(oldPosition);
            List<Pair> newPairs = GetPairsAt(newPosition);
            foreach (Pair pair in oldPairs)
            {
                if (!newPairs.Contains(pair))
                {
                    pairedObjects.Remove(pair);
                }
            }
            foreach (Pair pair in newPairs)
            {
                pairedObjects.Remove(pair);
                pairedObjects.Add(pair);
            }
        }
        else
        {//is周围的方块在动

            if (Vector2Int.Distance(_owner.Position, oldPosition) <= 1.1)
            {
                GridObject opposite;
                opposite = Map.Instance.GetGridObject(_owner.Position + _owner.Position - oldPosition);
                if (gridObject?.GetComponent<SemanticSymbol>() != null && opposite?.GetComponent<ObjectSymbol>() != null)
                {
                    Type objectType = opposite.GetComponent<ObjectSymbol>().refer;
                    Type semanticType = gridObject.GetComponent<SemanticSymbol>().refer;
                    pairedObjects.Remove(new Pair(objectType, semanticType));
                }
                else if (opposite?.GetComponent<SemanticSymbol>() != null && gridObject?.GetComponent<ObjectSymbol>() != null)
                {
                    Type objectType = gridObject.GetComponent<ObjectSymbol>().refer;
                    Type semanticType = opposite.GetComponent<SemanticSymbol>().refer;
                    pairedObjects.Remove(new Pair(objectType, semanticType));
                }
            }
            if (Vector2Int.Distance(_owner.Position, newPosition) <= 1.1)
            {
                GridObject opposite;
                opposite = Map.Instance.GetGridObject(_owner.Position + _owner.Position - newPosition);
                if (gridObject?.GetComponent<SemanticSymbol>() != null && opposite?.GetComponent<ObjectSymbol>() != null)
                {
                    Type objectType = opposite.GetComponent<ObjectSymbol>().refer;
                    Type semanticType = gridObject.GetComponent<SemanticSymbol>().refer;
                    pairedObjects.Add(new Pair(objectType, semanticType));
                }
                else if (opposite?.GetComponent<SemanticSymbol>() != null && gridObject?.GetComponent<ObjectSymbol>() != null)
                {
                    Type objectType = gridObject.GetComponent<ObjectSymbol>().refer;
                    Type semanticType = opposite.GetComponent<SemanticSymbol>().refer;
                    pairedObjects.Add(new Pair(objectType, semanticType));
                }
            }

        }
        if (!isPreparedToUpdating)
        {
            isPreparedToUpdating = true;
            StartCoroutine(UpdateSemanticComponent());
        }
        foreach (Pair pair in pairedObjects)
        {
            Debug.Log($"{pair.semanticType} {pair.objectType}");
        }
        
    }

    private void OnMovingEnd(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        UpdateSemantic(gridObject, oldPosition, newPosition);
    }

    private void OnRegisterPosition(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        UpdateSemantic(gridObject, oldPosition, newPosition);
    }

    private static bool isPreparedToUpdating = false;
    private IEnumerator UpdateSemanticComponent()
    {
        yield return new WaitForEndOfFrame();
        //更新所有
        OnSemanticRemove?.Invoke(typeof(Semantic), typeof(GridObject));
        yield return new WaitForEndOfFrame();
        foreach (Pair pair in pairedObjects)
        {
            OnSemanticAdd?.Invoke(pair.semanticType, pair.objectType);
        }

        isPreparedToUpdating = false;
    }
}

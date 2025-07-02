using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //Singleton
    public static Map Instance { get; private set; }

    private Dictionary<Vector2Int, GridObject> gridObjects = new Dictionary<Vector2Int, GridObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GridObject.OnMovingEnd += OnMovingEnd;
        GridObject.OnRegisterPosition += OnRegisterPostion;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GridObject GetGridObject(Vector2Int position)
    {
        if (gridObjects.TryGetValue(position, out GridObject gridObject))
        {
            return gridObject;
        }
        return null;
    }

    private void OnMovingEnd(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition)
    {
        if (gridObjects.TryGetValue(oldPosition, out GridObject existingObject) && existingObject == gridObject)
        {
            gridObjects.Remove(oldPosition);
        }
        gridObjects[newPosition] = gridObject;
    }

    private void OnRegisterPostion(GridObject gridObject, Vector2Int oldPosition, Vector2Int newPosition) {
        gridObjects[newPosition] = gridObject;
    }

}

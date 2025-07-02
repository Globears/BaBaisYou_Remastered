using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class InputHandler : MonoBehaviour
{
    public delegate void InputEventHandler(Vector2Int direction);
    public static event InputEventHandler OnInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnInput?.Invoke(Vector2Int.up);
            Debug.Log("W pressed, moving up");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnInput?.Invoke(Vector2Int.down);
            Debug.Log("S pressed, moving down");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnInput?.Invoke(Vector2Int.left);
            Debug.Log("A pressed, moving left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnInput?.Invoke(Vector2Int.right);
            Debug.Log("D pressed, moving right");
        }
    }
}

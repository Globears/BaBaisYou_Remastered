using UnityEngine;

public class Symbol_Stop : SemanticSymbol
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        refer = typeof(Stop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using UnityEngine;

public class Symbol_Rock : ObjectSymbol
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        refer = typeof(Rock);
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnSemanticRemove(Type semanticType, Type objectType)
    {
        
    }
}

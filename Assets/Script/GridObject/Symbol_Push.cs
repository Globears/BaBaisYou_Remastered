using System;
using UnityEngine;

public class Symbol_Push : SemanticSymbol
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        refer = typeof(Push);
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnSemanticRemove(Type semanticType, Type objectType)
    {
        
    }
}

using System;
using UnityEngine;

public abstract class SemanticSymbol : GridObject
{
    public Type refer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnSemanticRemove(Type semanticType, Type objectType)
    {
        //覆写，不会因为清空而清空自己的push属性
    }
}

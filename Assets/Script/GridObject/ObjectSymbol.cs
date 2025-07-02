using System;
using UnityEngine;

public abstract class ObjectSymbol : GridObject //符号额外有refer，表示该符号代表的物体或者语义类型
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

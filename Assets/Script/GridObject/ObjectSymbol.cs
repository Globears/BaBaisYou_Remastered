using System;
using UnityEngine;

public abstract class ObjectSymbol : GridObject //���Ŷ�����refer����ʾ�÷��Ŵ�������������������
{
    public Type refer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnSemanticRemove(Type semanticType, Type objectType)
    {
        //��д��������Ϊ��ն�����Լ���push����
    }
}

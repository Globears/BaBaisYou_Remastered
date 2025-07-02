using System;
using UnityEngine;

public class Symbol_Baba : ObjectSymbol
{
    public override void Awake()
    {
        base.Awake();
        refer = typeof(Baba);
    }

    protected override void Start()
    {
        Position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Move(Position);
        Semantic_Is.OnSemanticAdd += OnSemanticAdd;
        Semantic_Is.OnSemanticRemove += OnSemanticRemove;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnSemanticRemove(Type semanticType, Type objectType)
    {
        
    }
}

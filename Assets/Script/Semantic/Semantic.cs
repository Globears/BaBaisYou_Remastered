using System;
using UnityEngine;

public abstract class Semantic : MonoBehaviour
{
    

    protected GridObject _owner;

    public virtual void Awake()
    {
        _owner = GetComponent<GridObject>();
    }

}

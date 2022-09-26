using Unity.Entities;
using UnityEngine;



public struct CommonScripts : IComponentData
{
    public float Value;
}

public struct SingletonComp : IComponentData
{
    public float Value; 
}

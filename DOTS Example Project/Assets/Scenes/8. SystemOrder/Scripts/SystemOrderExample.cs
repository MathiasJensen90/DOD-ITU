using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(SystemOrdering2))]
public partial struct SystemOrderExample : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        
    }

    public void OnDestroy(ref SystemState state)
    {
       
    }

    public void OnUpdate(ref SystemState state)
    {
       
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial struct CreateAnEntity : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EntitiesExampleSingletonTag>();
        var e = state.EntityManager.CreateEntity(); 
        state.EntityManager.SetName(e, "myEntity");
    }
    
}

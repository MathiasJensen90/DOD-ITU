using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial struct CreateAnEntity : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EntitiesExampleSingletonTag>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        var entity = state.EntityManager.CreateEntity();
        state.EntityManager.SetName(entity, "myEntity");
        state.Enabled = false;
    }
    
}

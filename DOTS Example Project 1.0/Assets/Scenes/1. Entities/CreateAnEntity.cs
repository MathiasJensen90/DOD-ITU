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

    public void OnDestroy(ref SystemState state)
    {
       
    }

    public void OnUpdate(ref SystemState state)
    {
        var entityManager = state.EntityManager; 
        var e = entityManager.CreateEntity(); 
        entityManager.SetName(e, "myEntity");
        state.Enabled = false;
    }
}

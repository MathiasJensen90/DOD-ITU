using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial class CreateAnEntity : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<EntitiesExampleSingleton>();

    }

    protected override void OnUpdate()
    {
        var e = EntityManager.CreateEntity(); 
        EntityManager.SetName(e, "myEntity");
        Enabled = false;

    }
}

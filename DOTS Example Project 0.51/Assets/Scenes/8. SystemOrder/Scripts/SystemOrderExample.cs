using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(SystemOrdering2))]
public partial class SystemOrderExample : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<SystemOrderSingleton>();
    }
    protected override void OnUpdate()
    {
        Debug.Log("Hello, I'm SystemOrdering");
        Enabled = false;
    }
}

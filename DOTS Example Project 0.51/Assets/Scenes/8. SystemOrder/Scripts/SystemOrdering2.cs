using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(SystemOrdering3))]
public partial class SystemOrdering2 : SystemBase
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<SystemOrderSingleton>();
        }

        protected override void OnUpdate()
        {
            Debug.Log("Hello, I'm SystemOrdering2");
            Enabled = false;
        }
    }

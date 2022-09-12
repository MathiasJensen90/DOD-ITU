using Unity.Entities;
using UnityEngine;


    public partial class SystemOrdering3 : SystemBase
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<SystemOrderSingleton>();
        }
        protected override void OnUpdate()
        {
            Debug.Log("Hello, I'm SystemOrdering3");
            Enabled = false;
        }
    }

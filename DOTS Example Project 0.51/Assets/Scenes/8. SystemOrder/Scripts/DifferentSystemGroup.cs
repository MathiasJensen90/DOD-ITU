using Unity.Entities;
using UnityEngine;

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class DifferentSystemGroup : SystemBase
    {
        
         protected override void OnCreate()
        {
            RequireSingletonForUpdate<SystemOrderSingleton>();
        }
        protected override void OnUpdate()
        {
            
        }
    }

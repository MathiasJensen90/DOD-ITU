using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SystemOrderSingletonAuthoring : MonoBehaviour 
{
    class baker : Baker<SystemOrderSingletonAuthoring>
    {
        public override void Bake(SystemOrderSingletonAuthoring authoring)
        {
            AddComponent<SystemOrderSingleton>();
        }
    }
}
public struct SystemOrderSingleton : IComponentData
{
   
}

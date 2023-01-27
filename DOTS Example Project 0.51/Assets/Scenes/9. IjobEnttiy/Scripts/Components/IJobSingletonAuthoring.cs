using System;
using UnityEngine;
using Unity.Entities;


public class IJobSingletonAuthoring : MonoBehaviour 
{
    class Baker : Baker<IJobSingletonAuthoring>
    {
        public override void Bake(IJobSingletonAuthoring authoring)
        {
            AddComponent<IJobSingleton>();
        }
    }
}
public struct IJobSingleton : IComponentData
{
    
}

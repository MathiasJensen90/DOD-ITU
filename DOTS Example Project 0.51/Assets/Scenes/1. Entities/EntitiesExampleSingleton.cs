using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class EntitiesExampleSingleton : MonoBehaviour
{
    
    public class Baker : Baker<EntitiesExampleSingleton>
    {
        public override void Bake(EntitiesExampleSingleton authoring)
        {
            AddComponent<EntitiesExampleSingletonTag>();
        }
    }
}

public struct EntitiesExampleSingletonTag : IComponentData
{
    
}
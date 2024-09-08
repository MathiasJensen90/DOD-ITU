using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class RandomDataBaker : MonoBehaviour
{
    
    class baker : Baker<RandomDataBaker>
    {
        public override void Bake(RandomDataBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<RandomData>(entity); 
        }
    }
}


public struct RandomData : IComponentData
{
    public Random Value;
    public bool dataInitiliazed; 
    public float timer; 
}

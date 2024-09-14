using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class RandomDataBaker : MonoBehaviour
{
    public float duration = 2f;
    public bool example; 
    class baker : Baker<RandomDataBaker>
    {
        public override void Bake(RandomDataBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RandomData
            {
                duration = authoring.duration
            }); 
        }
    }
}


public struct RandomData : IComponentData
{
    public Random Value;
    public bool dataInitiliazed; 
    public float duration;
    public float timer;
}

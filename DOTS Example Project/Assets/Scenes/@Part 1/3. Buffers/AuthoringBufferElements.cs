using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class AuthoringBufferElements : MonoBehaviour
{
    public Transform[] enemyLocations;
    public class baker : Baker<AuthoringBufferElements>
    {
        public override void Bake(AuthoringBufferElements authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var buffer = AddBuffer<EnemyLocation>(entity);
            
            foreach (var enemeylocation in authoring.enemyLocations)
            {
                buffer.Add(new EnemyLocation { Value = enemeylocation.position });
            }
        }
    }
}

[InternalBufferCapacity(50)]
public struct EnemyLocation : IBufferElementData
{
    public float3 Value; 
}


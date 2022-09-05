using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class AuthoringBufferElements : MonoBehaviour, IConvertGameObjectToEntity
{
    public Transform[] enemyLocations;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        //add the buffer to the entity so we can store a list of float3 positions.
        //We use the transform of the gameobject to set the positions before it is converted
        var buffer = dstManager.AddBuffer<EnemyLocationBuffer>(entity);
        foreach (var enemeylocation in enemyLocations)
        {
            buffer.Add(new EnemyLocationBuffer {Value = enemeylocation.position});
        }
    }
}

public struct EnemyLocationBuffer : IBufferElementData
{
    public float3 Value; 
}


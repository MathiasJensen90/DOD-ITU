using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class SingletonComponent2Authoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public string firstName;
    public string lastName;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SingletonComponent2
        {
            firstname = this.firstName,
            lastName = this.lastName
        });
    }
}

public struct SingletonComponent2 : IComponentData
{
    public FixedString32Bytes firstname;
    public FixedString32Bytes lastName;
}

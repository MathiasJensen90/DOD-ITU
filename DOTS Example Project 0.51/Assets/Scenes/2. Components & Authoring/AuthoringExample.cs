using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class AuthoringExample : MonoBehaviour, IConvertGameObjectToEntity
{
    [Range(0,5)]
    public float simpleValue = 1;
    
    [Header("Character Stats")]
    public int health = 20;
    public float damage = 5;
    public float coldownTime = 2;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        //AddComponentData allows you to initialise the data
        dstManager.AddComponent<LifeTime>(entity);
        dstManager.AddComponentData(entity, new LifeTime {Value = simpleValue});
        dstManager.AddComponentData(entity, new CharactherStats
        {
            health = this.health * Random.Range(1, 3),
            damage = this.damage,
            coldownTime = this.coldownTime
        });
    }
}

public struct LifeTime : IComponentData
{
    public float Value;
}

public struct CharactherStats : IComponentData
{
    public int health;
    public float damage;
    public float coldownTime;
}



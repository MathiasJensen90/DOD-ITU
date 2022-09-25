using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public struct SeekerData : IComponentData
{
    public float3 direction;
    public float3 neareestTarget; 
}

public struct TargetData : IComponentData
{
    public float3 direction; 
}
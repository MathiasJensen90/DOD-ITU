using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct PhysicsPlayer : IComponentData
{
    public float moveSpeed;
    public int pickupAquirred;
}

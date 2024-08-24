using Unity.Entities;
using UnityEngine;

public struct moveData : IComponentData
{
    public float moveSpeed;
    public float rotationSpeed;
}
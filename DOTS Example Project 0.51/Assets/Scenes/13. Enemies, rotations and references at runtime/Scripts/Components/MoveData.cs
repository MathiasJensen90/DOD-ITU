using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct moveData : IComponentData
{
    public float moveSpeed;
    public float rotationSpeed;
}
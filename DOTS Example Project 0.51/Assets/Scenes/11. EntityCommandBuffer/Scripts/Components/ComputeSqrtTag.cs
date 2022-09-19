using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ComputeSqrt : IComponentData
{
    public float Value;
}

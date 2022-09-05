using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct DiverseComponent : IComponentData
{
    public float firstValue;
    public int secondValue;
}

using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct JumpingPlatform : IComponentData
{
    public float force; 
}

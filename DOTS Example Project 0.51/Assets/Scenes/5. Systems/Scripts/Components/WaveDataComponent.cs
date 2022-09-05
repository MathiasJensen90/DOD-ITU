using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct WaveDataComponent : IComponentData
{
   public float amplitude;
   public float frequency;
   public float rotationSpeed;
}

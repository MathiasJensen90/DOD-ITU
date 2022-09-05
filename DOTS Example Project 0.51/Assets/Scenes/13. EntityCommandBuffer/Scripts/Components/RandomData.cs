using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

[GenerateAuthoringComponent]
public struct RandomData : IComponentData
{
    public Random Value;
    public bool dataInitiliazed; 
    public float timer; 
}

using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct ComponentEntity : IComponentData
{
    public Entity Value; 
}

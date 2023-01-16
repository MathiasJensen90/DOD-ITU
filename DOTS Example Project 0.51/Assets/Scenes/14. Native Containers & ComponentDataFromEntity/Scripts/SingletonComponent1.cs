using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct SingletonComponent1 : IComponentData
{
   public Entity entityRef; 
}

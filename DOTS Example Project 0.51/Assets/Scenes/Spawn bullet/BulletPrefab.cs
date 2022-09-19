using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct bulletPrefab : IComponentData
{
   public Entity prefabTospawn;
   
}

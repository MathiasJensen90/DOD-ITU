using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Authoring;

public struct Raycaster : IComponentData
{
   public float rayLengt;
   public PhysicsCategoryTags collidesWith;
   public PhysicsCategoryTags belongsTo;
   
}

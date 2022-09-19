using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct DependencySingletonComponent : IComponentData
{
   public float radius;
   public int damage;
   public float distance;
   public Entity targetEntity;
}

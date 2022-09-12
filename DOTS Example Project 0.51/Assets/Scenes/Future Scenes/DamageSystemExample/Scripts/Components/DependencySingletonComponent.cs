using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct DependencySingletonComponent : IComponentData
{
   public float radius;
   public float distance;
   public int damage; 
   public Entity targetEntity;
}

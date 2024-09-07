using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


class DependencySingletonComponentAuthoring : MonoBehaviour
{
   public float radius;
   public int damage;
   public float distance;
   public GameObject target; 
   
    
   class baker : Baker<DependencySingletonComponentAuthoring>
   {
      public override void Bake(DependencySingletonComponentAuthoring authoring)
      {
         var entity = GetEntity(TransformUsageFlags.Renderable);
         AddComponent(entity, new DependencySingletonComponent
         {
            radius = authoring.radius,
            damage = authoring.damage,
            distance = authoring.distance,
            targetEntity = GetEntity(authoring.target, TransformUsageFlags.Dynamic)
         });
      }
   }
}


public struct DependencySingletonComponent : IComponentData
{
   public float radius;
   public int damage;
   public float distance;
   public Entity targetEntity;
}

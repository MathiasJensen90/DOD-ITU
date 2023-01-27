using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;


public class EntityManagerSingletonComponentAuthoring : MonoBehaviour
{
   public GameObject prefabToSpawn;
   public bool hasBuffer;
   public EntityManagerExample exampleType;
   class Baker : Baker<EntityManagerSingletonComponentAuthoring>
   {
      public override void Bake(EntityManagerSingletonComponentAuthoring authoring)
      {
         AddComponent( new EntityManagerSingletonComponent
         {
            prefabToSpawn = GetEntity(authoring.prefabToSpawn),
            hasBuffer = authoring.hasBuffer,
            exampleType = authoring.exampleType
         });
      }
   }
}

public struct EntityManagerSingletonComponent : IComponentData
{
   public Entity prefabToSpawn;
   public bool hasBuffer;
   public EntityManagerExample exampleType;
}


public enum EntityManagerExample{
   Simple,
   Complex
}

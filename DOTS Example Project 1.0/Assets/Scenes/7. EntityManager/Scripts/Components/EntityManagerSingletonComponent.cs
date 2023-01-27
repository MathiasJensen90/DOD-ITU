using System;using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public struct EntityManagerSingletonComponent : IComponentData
{
   public Entity prefabToSpawn;
   public bool hasBuffer;
   public EntityManagerExample ExampleType;
}


public enum EntityManagerExample{
   Simple,
   Complex
}

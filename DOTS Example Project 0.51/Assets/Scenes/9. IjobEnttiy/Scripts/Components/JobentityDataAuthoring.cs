using System;
using Unity.Entities;
using UnityEngine;

public class JobentityDataAuthoring : MonoBehaviour 
{
   class Baker : Baker<JobentityDataAuthoring>
   {
      public override void Bake(JobentityDataAuthoring authoring)
      {
         AddComponent<JobentityData>();
      }
   }
}
public struct JobentityData : IComponentData
{
   public float rotationValue;
}

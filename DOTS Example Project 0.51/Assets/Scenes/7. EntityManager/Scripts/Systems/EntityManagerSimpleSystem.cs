using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

    [UpdateAfter(typeof(GatherPlayerInput))]
    public partial class EntityManagerSimpleSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<EntityManagerSingletonComponent>();
        }
        protected override void OnUpdate()
        {
           var singleton = GetSingleton<EntityManagerSingletonComponent>();
           if (singleton.ExampleType != EntityManagerExample.Simple) return;
           
           Entities.ForEach((Entity entity, ref Translation trans, ref EntityManagerSingletonComponent singletonComp, in PlayerInputComponent input) =>
           {
         
               
               if (input.input1Value)
               {
                   EntityManager.RemoveComponent<RotateTag>(entity);
               }
               if (input.input2Value)
               {
                   EntityManager.AddComponent<RotateTag>(entity);
               }
               if (input.input3Value)
               {
                   EntityManager.AddComponentData(entity, new NonUniformScale
                   {
                       Value = new float3(2, 2, 2)
                   });
               }
               if (input.input4Value)
               {
                  
                  Entity e = EntityManager.Instantiate(singletonComp.prefabToSpawn);
                  EntityManager.SetComponentData(e, new Translation
                  {
                      Value = new float3(0, 1, 0)
                  });
               }
               if (input.input5Value)
               {
                   EntityManager.DestroyEntity(entity);
               }

           }).WithStructuralChanges().Run();
        }
    }

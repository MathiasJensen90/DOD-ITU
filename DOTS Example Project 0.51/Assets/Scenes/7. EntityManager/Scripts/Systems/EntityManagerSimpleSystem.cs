using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

    [UpdateAfter(typeof(GatherPlayerInput))]
    public partial struct EntityManagerSimpleSystem : ISystem
    {
        public  void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EntityManagerSingletonComponent>();
        }

        public void OnDestroy(ref SystemState state) { }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var singleton = SystemAPI.GetSingleton<EntityManagerSingletonComponent>();
           if (singleton.exampleType != EntityManagerExample.Simple) return;
           
           foreach(var(trans, singletonComp,input, entity) in SystemAPI.Query<RefRW<Translation>, RefRW<EntityManagerSingletonComponent>, RefRO<PlayerInputComponent>>().WithEntityAccess())
           {
               if (input.ValueRO.input1Value)
               {
                   ecb.RemoveComponent<RotateTag>(entity);
                   Debug.Log("ja");
               }
               if (input.ValueRO.input2Value)
               {
                   ecb.AddComponent<RotateTag>(entity);
               }
               if (input.ValueRO.input3Value)
               {
                   ecb.AddComponent(entity, new NonUniformScale
                   {
                       Value = new float3(2, 2, 2)
                   });
               }
               if (input.ValueRO.input4Value)
               {
                   Entity e = ecb.Instantiate(singletonComp.ValueRW.prefabToSpawn);
                  ecb.SetComponent(e, new Translation
                  {
                      Value = new float3(0, 1, 0)
                  });
               }
               if (input.ValueRO.input5Value)
               {
                   ecb.DestroyEntity(entity);
               }
               //ecb.Dispose();

           }
        }
    }

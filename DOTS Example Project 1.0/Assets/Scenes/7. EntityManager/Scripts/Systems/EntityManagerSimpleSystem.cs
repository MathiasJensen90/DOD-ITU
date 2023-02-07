using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


    public partial struct EntityManagerSimpleSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ManagerSingeltonComponent>();
        }

        public void OnDestroy(ref SystemState state)
        {
            
        }

        public void OnUpdate(ref SystemState state)
        {
            var singleton = SystemAPI.GetSingleton<ManagerSingeltonComponent>();
            if (singleton.ExampleType != EntityManagerExample.Simple) return;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);


            foreach (var (singelton, entity) in SystemAPI.Query<RefRW<ManagerSingeltonComponent>>().WithEntityAccess())
            {
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    ecb.RemoveComponent<RotateTag>(entity);
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ecb.AddComponent<RotateTag>(entity);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ecb.AddComponent(entity, new NonUniformScale
                    {
                        Value = new float3(2, 2, 2)
                    });
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                  
                    Entity e = state.EntityManager.Instantiate(singelton.ValueRO.prefabToSpawn);
                    ecb.SetComponent(e, new Translation
                    {
                        Value = new float3(0, 1, 0)
                    });
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }


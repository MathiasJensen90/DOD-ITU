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
             
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                  
                    Entity e = state.EntityManager.Instantiate(singelton.ValueRO.prefabToSpawn);
                    ecb.SetComponent(e, new LocalTransform
                    {
                        Position = new float3(0, 1, 0),
                        Rotation = quaternion.identity,
                        Scale = 1
                    });
                }
              
            }
            
            foreach (var (rotation, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithEntityAccess().WithAll<RotateTag>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Debug.Log("happening");
                    ecb.RemoveComponent<RotateTag>(entity);
                }
         
                if (Input.GetKeyDown(KeyCode.Alpha3))
                { 
                    ecb.AddComponent(entity, new LocalTransform
                    {
                        Scale = 2
                    });
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ecb.DestroyEntity(entity);
                }
            }
            foreach (var (rotation, entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithEntityAccess().WithNone<RotateTag>().WithAll<RotatingData>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ecb.AddComponent<RotateTag>(entity);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ecb.AddComponent(entity, new LocalTransform
                    {
                        Scale = 2
                    });
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    ecb.DestroyEntity(entity);
                }
            }

            
            ecb.Playback(state.EntityManager);
            
        }
    }


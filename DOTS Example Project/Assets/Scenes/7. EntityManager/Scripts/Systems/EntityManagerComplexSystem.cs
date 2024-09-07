using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial struct EntityManagerComplexSystem: ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ManagerSingeltonComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
         var singleTon = SystemAPI.GetSingleton<ManagerSingeltonComponent>();
        if (singleTon.ExampleType != EntityManagerExample.Complex) return;
        
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

        foreach (var (managersingleton, entity) in SystemAPI.Query<RefRW<ManagerSingeltonComponent>>().WithEntityAccess())
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Number of entities in the world: " + state.EntityManager.Debug.EntityCount);
                Debug.Log("Information about entity:" + state.EntityManager.Debug.GetEntityInfo(entity));
                if (managersingleton.ValueRO.hasBuffer) Debug.Log("Buffer length: " + SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).Length);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!managersingleton.ValueRO.hasBuffer)
                {
                    Debug.Log("Added a buffer to the entity");
                    ecb.AddBuffer<ListOfEntitiesCreatedComponent>(entity);
                    managersingleton.ValueRW.hasBuffer = true;
                }
                else
                {
                    Debug.Log("removed buffer from entity");
                    ecb.RemoveComponent<ListOfEntitiesCreatedComponent>(entity);
                    managersingleton.ValueRW.hasBuffer = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (managersingleton.ValueRO.hasBuffer)
                {
                    Debug.Log("Spawned entity and added it to a a buffer so we can keep track of it");
                    
                    //spawn an entity from a prefab and cache it so we can set its data
                    var entitySpawned = ecb.Instantiate(managersingleton.ValueRO.prefabToSpawn);
                    ecb.SetComponent(entitySpawned, new LocalTransform{Position = Random.insideUnitSphere * 2 });
                    
                    //get buffer and add the instantiated entity to it
                    var buffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity);
                    buffer.Add(new ListOfEntitiesCreatedComponent {entity = entitySpawned});
                }
                else
                {
                    Debug.LogWarning("You have not added a buffer yet!");
                }
               
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (managersingleton.ValueRO.hasBuffer)
                {
                    var buffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity); 
                    var bufferIndex = buffer.Length - 1;
                    if (bufferIndex >= 0)
                    {
                        ecb.DestroyEntity(buffer[bufferIndex].entity);
                        buffer.RemoveAt(bufferIndex);
                    }
                    else
                    {
                        Debug.LogWarning("You tried to delete an entity, but none exist in the buffer!");
                    }
                }

            }
            // else if (Input.GetKeyDown(KeyCode.Alpha5))
            // {
            //     if (managersingleton.ValueRO.hasBuffer && SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).Length != 0)
            //     {
            //         var buffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity);
            //         int lengthOffBuffer = buffer.Length - 1;
            //  
            //             var localEntity = buffer[lengthOffBuffer].entity; 
            //             if (!SystemAPI.HasComponent<NonUniformScale>(localEntity))
            //             {
            //                 ecb.AddComponent<NonUniformScale>(localEntity);
            //                 ecb.AddComponent(localEntity, new NonUniformScale {Value = new float3(1, 2, 1)});
            //             }
            //             else
            //             {
            //                 var currentScale = SystemAPI.GetComponent<NonUniformScale>(localEntity).Value;
            //                 ecb.AddComponent(localEntity, new NonUniformScale {Value = currentScale * 2});
            //             }
            //         
            //         Debug.Log("Changed the scale of the entitties in the buffer");
            //     }
            //     else
            //     {
            //         Debug.LogWarning("You have not added a buffer yet, or the buffer is empty");
            //     }
            // }
        }
        
        ecb.Playback(state.EntityManager);
    }
}

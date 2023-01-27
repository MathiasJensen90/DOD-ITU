using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class EntityManagerComplexSystem: SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<EntityManagerSingletonComponent>();
    }

    protected override void OnUpdate()
    {
        var singleTon = SystemAPI.GetSingleton<EntityManagerSingletonComponent>();
        if (singleTon.exampleType != EntityManagerExample.Complex) return;
        
        Entities.ForEach((Entity entity, ref EntityManagerSingletonComponent managerSingleton, in PlayerInputComponent inputComp) =>
        {
            if (inputComp.input1Value)
            {
                Debug.Log("Number of entities in the world: " + EntityManager.Debug.EntityCount);
                Debug.Log("Information about entity:" + EntityManager.Debug.GetEntityInfo(entity));
                if (managerSingleton.hasBuffer) Debug.Log("Buffer length: " + SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).Length);
            }
            else if (inputComp.input2Value)
            {
                if (!managerSingleton.hasBuffer)
                {
                    Debug.Log("Added a buffer to the entity");
                    EntityManager.AddBuffer<ListOfEntitiesCreatedComponent>(entity);
                    managerSingleton.hasBuffer = true;
                }
                else
                {
                    Debug.Log("removed buffer from entity");
                    EntityManager.RemoveComponent<ListOfEntitiesCreatedComponent>(entity);
                    managerSingleton.hasBuffer = false;
                }
            }
            else if (inputComp.input3Value)
            {
                if (managerSingleton.hasBuffer)
                {
                    Debug.Log("Spawned entity and added it to a a buffer so we can keep track of it");
                    
                    //spawn an entity from a prefab and cache it so we can set its data
                    var entitySpawned = EntityManager.Instantiate(managerSingleton.prefabToSpawn);
                    EntityManager.SetComponentData(entitySpawned, new Translation{Value = Random.insideUnitSphere * 2 });
                    
                    //get buffer and add the instantiated entity to it
                    var buffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity);
                    buffer.Add(new ListOfEntitiesCreatedComponent {entity = entitySpawned});
                }
                else
                {
                    Debug.LogWarning("You have not added a buffer yet!");
                }
               
            }
            else if (inputComp.input4Value)
            {
                if (managerSingleton.hasBuffer)
                {
                    var lengthOffBuffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).Length - 1;
                    if (lengthOffBuffer >= 0)
                    {
                        EntityManager.DestroyEntity(SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity)[lengthOffBuffer]
                            .entity);
                        SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).RemoveAt(lengthOffBuffer);
                    }
                    else
                    {
                        Debug.LogWarning("You tried to delete an entity, but none exist in the buffer!");
                    }
                }

            }
            else if (inputComp.input5Value)
            {
                if (managerSingleton.hasBuffer && SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity).Length != 0)
                {
                    var buffer = SystemAPI.GetBuffer<ListOfEntitiesCreatedComponent>(entity);
                    int lengthOffBuffer = buffer.Length - 1;
             
                        var localEntity = buffer[lengthOffBuffer].entity; 
                        if (!SystemAPI.HasComponent<NonUniformScale>(localEntity))
                        {
                            EntityManager.AddComponent<NonUniformScale>(localEntity);
                            EntityManager.AddComponentData(localEntity, new NonUniformScale {Value = new float3(1, 2, 1)});
                        }
                        else
                        {
                            var currentScale = SystemAPI.GetComponent<NonUniformScale>(localEntity).Value;
                            EntityManager.AddComponentData(localEntity, new NonUniformScale {Value = currentScale * 2});
                        }
                    
                    Debug.Log("Changed the scale of the entitties in the buffer");
                }
                else
                {
                    Debug.LogWarning("You have not added a buffer yet, or the buffer is empty");
                }
              
            }
            else if (inputComp.input6Value)
            {
                var e = EntityManager.CreateEntity();
                EntityManager.SetName(e, "MyCreatedEntity");
                EntityManager.AddComponentData(e, new CharacterStats
                {
                    damage = 1,
                    health = 10,
                    coldownTime = 1
                });
                Debug.Log("Created a new entity and added a component");
            }

            //Use WithStructuralChanges when: You are adding/removing components from an entity or creating an entity
            //use WithoutBurst when: You are accessing reference fields in you entities.foreach.If you can, try to cache them outside the entities foreach - for example deltaTime
        }).WithoutBurst().WithStructuralChanges().Run();
    }
}

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[DisableAutoCreation]
public partial struct CommonFunctionsExample : ISystem 
{
    public void OnUpdate(ref SystemState state)
    {
        ComponentLookup<LocalTransform> localTrans = SystemAPI.GetComponentLookup<LocalTransform>();
        
        EntityQuery query = state.GetEntityQuery(ComponentType.Exclude<Bullet>(), ComponentType.ReadOnly<LocalTransform>());
        var entityArray = query.ToEntityArray(Allocator.Persistent);
        var componetArray = query.ToComponentDataArray<LocalTransform>(Allocator.Persistent);

        Entity newEntity = state.EntityManager.CreateEntity(); 
        bool hasRot = localTrans.HasComponent(newEntity);
        
        state.EntityManager.DestroyEntity(newEntity);

        if (newEntity == Entity.Null)
        {
            Entity SecondEntity = state.EntityManager.CreateEntity(); 
        }
        
        if (localTrans.TryGetComponent(newEntity, out LocalTransform trans))
        {
            trans.Rotation = new quaternion(1, 1, 1, 1);
        };

        var oneEntity = SystemAPI.GetSingletonEntity<PhysicsPlayer>();
        var oneComp = SystemAPI.GetSingleton<Bullet>();
    }
    
}

public partial struct BlandJob : IJobEntity
{
    [ReadOnly] public ComponentLookup<LocalTransform> transform;
    public Entity eRef;
    
    public void Execute(ref LocalTransform trans)
    {
        trans.Position = transform[eRef].Position; 

    }
}
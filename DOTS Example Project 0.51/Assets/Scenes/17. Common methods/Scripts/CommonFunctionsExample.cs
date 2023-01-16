using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[DisableAutoCreation]
public partial class CommonFunctionsExample : SystemBase 
{
    protected override void OnUpdate()
    {
        ComponentLookup<Rotation> rotations = GetComponentLookup<Rotation>();
        ComponentLookup<Translation> translations = GetComponentLookup<Translation>(true);
        
        EntityQuery query = GetEntityQuery(ComponentType.Exclude<Bullet>(), ComponentType.ReadOnly<Translation>());
        var entityArray = query.ToEntityArray(Allocator.Persistent);
        var componetArray = query.ToComponentDataArray<Translation>(Allocator.Persistent);

        Entity newEntity = EntityManager.CreateEntity();
        bool hasRot = rotations.HasComponent(newEntity);
        
        EntityManager.DestroyEntity(newEntity);

        if (newEntity == Entity.Null)
        {
            Entity SecondEntity = EntityManager.CreateEntity();
        }
        
        if (rotations.TryGetComponent(newEntity, out Rotation rotComp))
        {
            rotComp.Value = new quaternion(1, 1, 1, 1);
        };

        var oneEntity = SystemAPI.GetSingletonEntity<PhysicsPlayer>();
        var oneComp = SystemAPI.GetSingleton<Bullet>();
    }
}

public partial struct BlandJob : IJobEntity
{
    [ReadOnly] public ComponentLookup<Translation> translations;
    public Entity eRef;
    
    public void Execute(ref Translation trans)
    {
        trans.Value = translations[eRef].Value; 

    }
}
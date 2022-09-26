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
        ComponentDataFromEntity<Rotation> rotations = GetComponentDataFromEntity<Rotation>();
        ComponentDataFromEntity<Translation> translations = GetComponentDataFromEntity<Translation>(true);
        
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


        var oneEntity = GetSingletonEntity<PhysicsPlayer>();
        var oneComp = GetSingleton<Bullet>();


    }
}

public partial struct BlandJob : IJobEntity
{
    [ReadOnly] public ComponentDataFromEntity<Translation> translations;
    public Entity eRef;
    
    public void Execute(ref Translation trans)
    {
        trans.Value = translations[eRef].Value; 

    }
}
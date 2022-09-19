using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class QueriesExamplesSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<QuerySingleton>();
    }

    protected override void OnUpdate()
    {

        //NativeArray<int> myArray = new NativeArray<int>(100, World.UpdateAllocator.ToAllocator);

        EntityQuery newEntityquery = GetEntityQuery(ComponentType.ReadOnly<HeroTag>());
        float dt = Time.DeltaTime;
        
        //this query is the same as the one below it
        EntityQuery query = GetEntityQuery(ComponentType.ReadWrite<Translation>(), ComponentType.ReadOnly<Rotation>());

        Entities.ForEach((ref Translation translation, in Rotation rotation) =>
        {
            translation.Value += dt * new float3(0, 0.4f, 0);

        }).Schedule();

        
        EntityQuery query2 = GetEntityQuery(ComponentType.ReadOnly<TagComponent1>(), ComponentType.ReadOnly<TagComponent2>(), ComponentType.ReadOnly<RotateTag>());
        
        Entities.WithAll<TagComponent1, TagComponent2>().ForEach(( ref Rotation rotation) =>
        {
            var xRot = quaternion.RotateX(80 * Mathf.Deg2Rad * dt);
            rotation.Value = math.mul(rotation.Value, xRot);
        }).Schedule();
        
        
        EntityQuery query3 = GetEntityQuery(ComponentType.Exclude<TagComponent1>(), ComponentType.ReadOnly<TagComponent2>());
        
        Entities.WithAll<TagComponent2>().WithNone<TagComponent1>().ForEach((ref Rotation rotation) =>
        {
            var xRot = quaternion.RotateY(80 * Mathf.Deg2Rad * dt);
            rotation.Value = math.mul(rotation.Value, xRot);
        }).Schedule();

        EntityQueryDesc myQueryDesc = new EntityQueryDesc
        {
            Any = new[] {ComponentType.ReadOnly<TagComponent3>(), ComponentType.ReadOnly<CooldownComponent>()},
            All = new[] {ComponentType.ReadWrite<Rotation>()},

        };
        
        Entities.WithAny<TagComponent3, CooldownComponent>().ForEach((ref Rotation rotation) =>
        {
            var xRot = quaternion.RotateZ(80 * Mathf.Deg2Rad * dt);
            rotation.Value = math.mul(rotation.Value, xRot);
        }).Schedule();
        
    }
}

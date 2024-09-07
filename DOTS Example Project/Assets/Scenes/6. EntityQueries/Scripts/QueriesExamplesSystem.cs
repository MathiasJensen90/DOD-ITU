using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



public partial struct QueriesExamplesSystem : ISystem
{
    
    [BurstCompile]   
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<QuerySingleton>();
    }
    
    [BurstCompile]   
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        
        //this query is the same as the one below it
        //EntityQuery query = state.GetEntityQuery(ComponentType.ReadWrite<LocalTransform>());

        foreach (var trans in SystemAPI.Query<RefRW<LocalTransform>>())
        {
            trans.ValueRW.Position += new float3(0, 0.004f, 0) * dt;
        }
        
        
        //EntityQuery query2 = state.GetEntityQuery(ComponentType.ReadOnly<TagComponent1>(), ComponentType.ReadOnly<TagComponent2>(), ComponentType.ReadOnly<RotateTag>());
        foreach (var trans in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TagComponent1>())
        {
            var xRot = quaternion.RotateX(80 * Mathf.Deg2Rad * dt);
            trans.ValueRW.Rotation = math.mul(trans.ValueRO.Rotation, xRot);
        }
        
        //EntityQuery query3 = state.GetEntityQuery(ComponentType.Exclude<TagComponent1>(), ComponentType.ReadOnly<TagComponent2>());
        foreach (var trans in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TagComponent2>().WithNone<TagComponent1>())
        {
            var xRot = quaternion.RotateY(80 * Mathf.Deg2Rad * dt);
            trans.ValueRW.Rotation = math.mul(trans.ValueRO.Rotation, xRot);
        }

        
        // EntityQueryDesc myQueryDesc = new EntityQueryDesc
        // {
        //     Any = new[] {ComponentType.ReadOnly<TagComponent3>(), ComponentType.ReadOnly<TagComponent2>()},
        //     All = new[] {ComponentType.ReadWrite<LocalTransform>()},
        //
        // };
        
        foreach (var trans in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<TagComponent2>().WithAll<TagComponent3>())
        {
            var xRot = quaternion.RotateZ(80 * Mathf.Deg2Rad * dt);
            trans.ValueRW.Rotation = math.mul(trans.ValueRO.Rotation, xRot);
        }
    }
}

// public partial struct MovementJob : IJobEntity
// {
//     public void Execute(ref LocalTransform trans)
//     {
//         trans.Position += new float3(0, 0.001f, 0);
//     }
// }
//
// [WithAny(typeof(TagComponent1))]
// public partial struct RotateJob1 : IJobEntity
// {
//     public float dt; 
//     public void Execute( ref LocalTransform trans)
//     {
//         var xRot = quaternion.RotateX(80 * Mathf.Deg2Rad * dt);
//         trans.Rotation = math.mul(trans.Rotation, xRot);
//     }
// }
// [WithAll( typeof(TagComponent2))]
// [WithNone( typeof(TagComponent3))]
// public partial struct RotateJob2 : IJobEntity
// {
//     public float dt; 
//     public void Execute( ref LocalTransform trans)
//     {
//         var xRot = quaternion.RotateY(80 * Mathf.Deg2Rad * dt);
//         trans.Rotation = math.mul(trans.Rotation, xRot);
//     }
// }
//
// [WithAll(typeof(TagComponent3), typeof(TagComponent2))]
// public partial struct RotateJob3 : IJobEntity
// {
//     public float dt; 
//     public void Execute([ChunkIndexInQuery] int i,  ref LocalTransform transform)
//     {
//         var xRot = quaternion.RotateZ(80 * Mathf.Deg2Rad * dt);
//         transform.Rotation = math.mul(transform.Rotation, xRot);
//     }
// }
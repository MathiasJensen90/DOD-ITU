using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


[UpdateAfter(typeof(FaceTargetSystem))]
public partial struct EneemyMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameplayInteractionSingleton>();
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime; 

         new EnemyMovementJob
        {
            dt = dt
        }.Schedule();
    }
}


//
// [UpdateAfter(typeof(FaceTargetSystem))]
// public partial class EneemyMovementSystem1 : SystemBase
// {
//     protected override void OnUpdate()
//     {
//         float dt = SystemAPI.Time.DeltaTime; 
//
//         var enemyMovementJob = new EnemyMovementJob
//         {
//             dt = dt
//         }.Schedule(Dependency);
//         
//         enemyMovementJob.Complete();
//     }
// }

[WithAll(typeof(ChaserTag))]
[BurstCompile]
public partial struct EnemyMovementJob : IJobEntity
{
    public float dt; 
    public void Execute(ref LocalTransform trans, in moveData moveData)
    {
        float3 forwardDir = math.forward(trans.Rotation.value);
        trans.Position += forwardDir * moveData.moveSpeed * dt;
    }
}
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct PickupRotate : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        
        state.Dependency = new PickupRotationJob
        {
            dt = dt
        }.ScheduleParallel(state.Dependency);
    }
}


[BurstCompile]
[WithAll(typeof(PickupTag))]
public partial struct PickupRotationJob : IJobEntity
{
    public float dt; 
    public void Execute(ref LocalTransform transform)
    {
        var rotateVal = math.radians(new float3(0, 90, 0)) * dt;
        transform.Rotation = math.mul(transform.Rotation ,  quaternion.Euler(rotateVal));
    }
}

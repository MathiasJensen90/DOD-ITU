using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class IJobEntityExample : SystemBase 
{
    protected override void OnCreate()
    {
        RequireForUpdate<IJobSingleton>();
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        var ecb = new EntityCommandBuffer(Allocator.TempJob); 
        
        RotationJob rotationJob = new RotationJob
        {
            deltaTime = deltaTime
        };
        
        

        JobHandle rotationJobhandle = rotationJob.Schedule(Dependency);

        rotationJob.Schedule();
        rotationJobhandle.Complete();

    //     #region EFE
    //     Entities.ForEach((ref Rotation rot, in JobentityData jobentityData) =>
    //     {
    //         rot.Value = math.mul(math.normalize(rot.Value),
    //             quaternion.AxisAngle(math.up(), math.radians(jobentityData.rotationValue) * deltaTime));
    //     }).Schedule();
    //     #endregion
     }
}


[WithAny(typeof(CompositeScale))]
public partial struct RotationJob : IJobEntity
{
    public float deltaTime;
    public void Execute(Entity e, ref Rotation rot, in JobentityData jobentityData)
    {
        rot.Value = math.mul(math.normalize(rot.Value),
            quaternion.AxisAngle(math.up(), math.radians(jobentityData.rotationValue) * deltaTime));
    }
}
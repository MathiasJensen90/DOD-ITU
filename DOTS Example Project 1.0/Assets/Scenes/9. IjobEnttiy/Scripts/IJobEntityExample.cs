using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class IJobEntityExample : SystemBase 
{
    protected override void OnCreate()
    {
        RequireForUpdate<IJobSingleton>();
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;  
        
        RotationJob rotationJob = new RotationJob
        {
            deltaTime = deltaTime
        };

        JobHandle rotationJobhandle = rotationJob.Schedule(Dependency);

        rotationJob.Schedule();

        
        rotationJobhandle.Complete();

        // #region EFE
        // Entities.ForEach((ref Rotation rot, in JobentityData jobentityData) =>
        // {
        //     rot.Value = math.mul(math.normalize(rot.Value),
        //         quaternion.AxisAngle(math.up(), math.radians(jobentityData.rotationValue) * deltaTime));
        // }).Schedule();
        // #endregion
    }
}


public partial struct RotationJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref Rotation rot, in JobentityData jobentityData)
    {
        rot.Value = math.mul(math.normalize(rot.Value),
            quaternion.AxisAngle(math.up(), math.radians(jobentityData.rotationValue) * deltaTime));
    }
}
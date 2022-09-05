using System.Data;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class RotationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;

        Entities.WithAll<RotateTag>().WithNone<StopRotatingTag>().ForEach((ref Rotation rotation, in WaveDataComponent waveData) =>
        {
            var xRot = quaternion.RotateX( waveData.rotationSpeed * Mathf.Deg2Rad * dt);
            
            rotation.Value = math.mul(rotation.Value, xRot);
        }).Schedule();
    }
}

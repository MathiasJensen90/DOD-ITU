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
        float dt = SystemAPI.Time.DeltaTime; 

        Entities.WithAll<RotateTag>().WithNone<StopRotatingTag>().ForEach((ref Rotation rotation, in RotatingData rotateData) =>
        {
            var xRot = quaternion.RotateX( rotateData.Value * Mathf.Deg2Rad * dt);
            
            rotation.Value = math.mul(rotation.Value, xRot);
        }).Schedule();
    }
}

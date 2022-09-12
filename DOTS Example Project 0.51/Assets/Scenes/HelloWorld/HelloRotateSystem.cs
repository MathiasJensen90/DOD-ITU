using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class HelloRotateSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        
        Entities.ForEach((ref Rotation rot, in Translation trans, in RotateSpeed rotateComp ) =>
        {
            
            var xRot = quaternion.RotateX( rotateComp.value * Mathf.Deg2Rad * dt);
            
            rot.Value = math.mul(rot.Value, xRot);

        }).Run();
    }
}

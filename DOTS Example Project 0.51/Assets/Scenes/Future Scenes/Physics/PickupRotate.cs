using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class PickupRotate : SystemBase
{
    protected override void OnUpdate()
    {


        float dt = Time.DeltaTime;
        
        Entities.WithAll<PickupTag>().ForEach((ref Translation translation, ref Rotation rotation) =>
        {
            var rotateVal = math.radians(new float3(0, 90, 0)) * dt;
            rotation.Value = math.mul(rotation.Value,  quaternion.Euler(rotateVal));
        }).Schedule();
    }
}

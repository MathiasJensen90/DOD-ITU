using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class UpdateCameraTargetPositionSystem : SystemBase
{
    protected override void OnUpdate()
    {

        Entities.WithAll<PhysicsPlayer>().ForEach((ref Translation translation) =>
        {
            SimpleCameraFollow.Instance.UpdateTargetPosition(translation.Value);

        }).WithoutBurst().Run();
    }
}

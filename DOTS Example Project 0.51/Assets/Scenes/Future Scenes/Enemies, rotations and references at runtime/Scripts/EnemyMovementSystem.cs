using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(FaceTargetSystem))]
public partial class EneemyMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        Entities.WithAll<ChaserTag>().ForEach((ref Translation translation, in Rotation rot, in moveData moveData) =>
        {
            float3 forwardDir = math.forward(rot.Value);
            translation.Value +=  forwardDir * moveData.moveSpeed * dt;
        }).ScheduleParallel();
    }
}

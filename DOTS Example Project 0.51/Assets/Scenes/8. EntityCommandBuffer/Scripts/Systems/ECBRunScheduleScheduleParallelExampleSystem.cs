using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(SpawnSystem))]
public partial class ECBRunScheduleScheduleParallelExampleSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<ECBSingletonComponent>();
    }

    protected override void OnUpdate()
    {
        var singletonComp = GetSingleton<ECBSingletonComponent>().SchedulingType;

        switch (singletonComp)
        {
            case SchedulingType.Run:
                Entities.ForEach((Entity entity, ref ComputeSqrt compSqt) =>
                {
                     var computeSquareRot = math.sqrt(12342);
                     var computeSquareRot2 = math.sqrt(223423432);
                     var computeSquareRot3 = math.sqrt(511);
                     compSqt.Value = computeSquareRot * computeSquareRot2 * computeSquareRot3;

                }).Run();
                break;
            
            case SchedulingType.Schedule:
                Entities.ForEach((Entity entity, ref ComputeSqrt compSqt) =>
                {
                    var computeSquareRot = math.sqrt(12342);
                    var computeSquareRot2 = math.sqrt(223423432);
                    var computeSquareRot3 = math.sqrt(511);
                    compSqt.Value = computeSquareRot * computeSquareRot2 * computeSquareRot3;

                }).Schedule();
                break;

            case SchedulingType.ScheduleParallel:
                Entities.ForEach((Entity entity, ref ComputeSqrt compSqt) =>
                {
                    var computeSquareRot = math.sqrt(12342);
                    var computeSquareRot2 = math.sqrt(223423432);
                    var computeSquareRot3 = math.sqrt(511);
                    compSqt.Value = computeSquareRot * computeSquareRot2 * computeSquareRot3;

                }).ScheduleParallel();
                break;
        }
    }
}

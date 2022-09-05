using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class WaveMoveSystem : SystemBase
{
    protected override void OnCreate()
    {
       RequireSingletonForUpdate<SystemExampleSingleton>();
    }

    protected override void OnUpdate()
    {
        var elapsedTime = Time.ElapsedTime;

        Entities.ForEach(
            (Entity entity, ref Translation trans, ref Rotation rot, in WaveDataComponent waveData) =>
            {
                var waveMovement = waveData.amplitude * math.sin((float)elapsedTime * waveData.frequency) + trans.Value.y;
                trans.Value = new float3(trans.Value.x, waveMovement, trans.Value.z);
                //EntityManager.RemoveComponent<WaveDataComponent>(entity);

            

            }).ScheduleParallel();

       
    }
}

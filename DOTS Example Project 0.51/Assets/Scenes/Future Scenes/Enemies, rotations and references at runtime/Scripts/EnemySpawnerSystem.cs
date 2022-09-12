using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<GameplayInteractionSingleton>();
    }

    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        var elapsedTime = (uint)Time.ElapsedTime;
        
        Entities.ForEach((Entity entity, ref ChaserSpawner chaserSpawn, in Translation trans) =>
        {
            chaserSpawn.timer -= dt;
            if (chaserSpawn.timer < 0)
            {
                chaserSpawn.timer = chaserSpawn.timerDelay;
                Entity e = EntityManager.Instantiate(chaserSpawn.chaser); 
                EntityManager.SetComponentData(e, new Translation
                {
                    Value = trans.Value + new float3(
                        chaserSpawn.random.NextFloat(-11, 11),
                        0,
                        chaserSpawn.random.NextFloat(-8, 8))       
                });
                EntityManager.SetComponentData(e, new moveData
                {
                    moveSpeed = chaserSpawn.random.NextFloat(2, 6),
                    rotationSpeed = chaserSpawn.random.NextFloat(.3f, .7f)
                });
            }

        }).WithStructuralChanges().Run();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[UpdateAfter(typeof(ApplyDamageSystem))]
public partial class ResolveDamageSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem ecbSystem; 
    protected override void OnCreate()
    {

        ecbSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        var ecb = ecbSystem.CreateCommandBuffer();
        var parallelEcb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

       var killandbla = new KillOrAddColldownJob
        {
            ecb = ecb
        }.Schedule();

       var cooldownTickJob = new CooldownTickJob
       {
           dt = dt,
           ecb = parallelEcb
       }.ScheduleParallel(killandbla);
        
        ecbSystem.AddJobHandleForProducer(cooldownTickJob);
    }
}

[WithAll(typeof(TookDamage))]
[WithNone(typeof(CooldownTag))]
[BurstCompile]
public partial struct KillOrAddColldownJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public void Execute(Entity entity, ref EnemyComponent enemy, ref TookDamage tookDamage)
    {
        enemy.health -= tookDamage.Value;
        if (enemy.health <= 0)
        {
            ecb.DestroyEntity(entity);
        }
        else
        {
            ecb.AddComponent<CooldownTag>(entity);
        }
    }
}

[WithAll(typeof(CooldownTag))]
[BurstCompile]
public partial struct CooldownTickJob : IJobEntity
{
    public float dt; 
    public EntityCommandBuffer.ParallelWriter ecb; 
    public void Execute(Entity entity, [EntityInQueryIndex]int index,  ref EnemyComponent enemyComponent)
    {
        enemyComponent.takingDamageColdown -= dt;
        if (enemyComponent.takingDamageColdown <= 0)
        {
            enemyComponent.takingDamageColdown = 4; 
            ecb.RemoveComponent<CooldownTag>(index, entity);
            ecb.RemoveComponent<TookDamage>(index, entity);
        }
    }
}
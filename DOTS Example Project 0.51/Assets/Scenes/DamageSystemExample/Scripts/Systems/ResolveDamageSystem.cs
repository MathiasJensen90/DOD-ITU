using System.Collections;
using System.Collections.Generic;
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

         Entities.WithAll<TookDamage>().WithNone<CooldownTag>().ForEach((Entity entity, ref EnemyComponent enemy, ref TookDamage tookDamage) =>
        {
            enemy.health -= tookDamage.Value;
            if (enemy.health <= 0)
            {
                EntityManager.DestroyEntity(entity);
            }
            else
            {
                EntityManager.AddComponent<CooldownTag>(entity);
            }
        }).WithStructuralChanges().Run();
       
         Entities.WithAll<CooldownTag>().WithName("CooldownSystem").ForEach((Entity entity, int entityInQueryIndex,  ref EnemyComponent enemyComponent) =>
        {
            enemyComponent.takingDamageColdown -= dt;
            if (enemyComponent.takingDamageColdown <= 0)
            {
                enemyComponent.takingDamageColdown = 4; 
                EntityManager.RemoveComponent<CooldownTag>(entity);
                EntityManager.RemoveComponent<TookDamage>(entity);
            }
        }).WithStructuralChanges().Run();
        
         // TO DO Show better version of doing this without sttructural changes

        

        #region withECB

        
       //
       //
       //  var ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();
       //  float dt = Time.DeltaTime;
       //
       //    Entities.WithAll<TookDamage>().WithNone<CooldownTag>().ForEach((Entity entity, int entityInQueryIndex, ref EnemyComponent enemy, ref TookDamage tookDamage) =>
       //  {
       //      enemy.health -= tookDamage.Value;
       //      if (enemy.health <= 0)
       //      {
       //          ecb.DestroyEntity(entityInQueryIndex, entity);
       //      }
       //      else
       //      {
       //          ecb.AddComponent<CooldownTag>(entityInQueryIndex, entity);
       //      }
       //  }).Schedule(Dependency);
       //
       //  Entities.WithAll<CooldownTag>().WithName("CooldownSystem").ForEach((Entity entity, int entityInQueryIndex,  ref EnemyComponent enemyComponent) =>
       // {
       //     enemyComponent.takingDamageColdown -= dt;
       //     if (enemyComponent.takingDamageColdown <= 0)
       //     {
       //         enemyComponent.takingDamageColdown = 4; 
       //         ecb.RemoveComponent<CooldownTag>(entityInQueryIndex, entity);
       //         ecb.RemoveComponent<TookDamage>(entityInQueryIndex, entity);
       //     }
       // }).Schedule();
       //
       //  ecbSystem.AddJobHandleForProducer(Dependency);
       #endregion
       
    }
}

using Unity.Collections;
using Unity.Entities;
using UnityEngine;


    public partial class TargetInitSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<GameplayInteractionSingleton>();
        }

        protected override void OnUpdate()
        {
            var tower = GetSingletonEntity<TowerTag>();
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            
            Entities.WithAll<TowerTag>().WithNone<EnemyTargetBuffer>().ForEach((Entity entity) =>
            {
                EntityManager.AddBuffer<EnemyTargetBuffer>(entity);
            }).WithStructuralChanges().Run();

            Entities.WithNone<TowerTarget>().WithAll<ChaserTag>().ForEach((Entity entity) =>
            {
                ecb.AddComponent(entity, new TowerTarget
                {
                    Value = tower
                });

                var bufferFromTower = GetBuffer<EnemyTargetBuffer>(tower);
                bufferFromTower.Add(new EnemyTargetBuffer
                {
                    Value = entity
                });
            }).Schedule();

            Dependency.Complete();
            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }




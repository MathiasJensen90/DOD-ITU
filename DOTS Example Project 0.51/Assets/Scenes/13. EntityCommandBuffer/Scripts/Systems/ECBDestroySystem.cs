using Unity.Entities;
using Unity.Rendering;
using UnityEngine;


[UpdateAfter(typeof(ECBStopRotationSystem))]
    public partial class ECBDestroySystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem ecbSystem;
   
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<ECBSingletonComponent>();
            ecbSystem = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var elapsedTime = Time.ElapsedTime;
            EntityCommandBuffer.ParallelWriter ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.ForEach((Entity entity, int entityInQueryIndex, in StopRotatingTag stopRot) =>
            {
                if (stopRot.timer >= elapsedTime) return;
                ecb.DestroyEntity(entityInQueryIndex, entity);

            }).Schedule();
            
            ecbSystem.AddJobHandleForProducer(Dependency);
        }
    }

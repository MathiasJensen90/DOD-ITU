using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

public partial class TriggerSystem : SystemBase
{

    private EndSimulationEntityCommandBufferSystem endECBSystem;
    private StepPhysicsWorld stepPhysicsWorld;
    
    protected override void OnCreate()
    {
        endECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override void OnUpdate()
    {
        var triggerJob = new TriggerJob
        {
            allPickups = GetComponentDataFromEntity<PickupTag>(true),
            allPlayers = GetComponentDataFromEntity<PhysicsPlayer>(),
            ecb = endECBSystem.CreateCommandBuffer()
        }.Schedule(stepPhysicsWorld.Simulation, Dependency);

        triggerJob.Complete();
        endECBSystem.AddJobHandleForProducer(Dependency);
    }
}


[BurstCompile]
struct TriggerJob : ITriggerEventsJob
{
    public ComponentDataFromEntity<PhysicsPlayer> allPlayers;
    [ReadOnly] public ComponentDataFromEntity<PickupTag> allPickups;
    public EntityCommandBuffer ecb; 

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        if (allPickups.HasComponent(entityA) && allPlayers.HasComponent(entityB))
        {
            var newPickupAquirred = allPlayers[entityB];
            newPickupAquirred.pickupAquirred++;
            allPlayers[entityB] = newPickupAquirred;
            ecb.DestroyEntity(entityA);
        }
        else if(allPickups.HasComponent(entityB) && allPlayers.HasComponent(entityA))
        {
            var newPickupAquirred = allPlayers[entityA];
            newPickupAquirred.pickupAquirred++;
            allPlayers[entityA] = newPickupAquirred;
            ecb.DestroyEntity(entityB);
        }
    }
}

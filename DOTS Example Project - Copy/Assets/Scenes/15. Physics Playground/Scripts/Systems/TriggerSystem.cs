using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
[RequireMatchingQueriesForUpdate]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
//[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct TriggerSystem : ISystem
{

    ComponentLookup<PickupTag> allPickups;
    ComponentLookup<PhysicsPlayer> allPlayers;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
        allPickups = state.GetComponentLookup<PickupTag>(true);
        allPlayers = state.GetComponentLookup<PhysicsPlayer>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        allPickups.Update(ref state);
        allPlayers.Update(ref state);
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        state.Dependency = new TriggerJob
        {
            allPickups = allPickups,
            allPlayers = allPlayers,
            ecb = ecb
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
}


[BurstCompile]
struct TriggerJob : ITriggerEventsJob
{
    public ComponentLookup<PhysicsPlayer> allPlayers;
    [ReadOnly] public ComponentLookup<PickupTag> allPickups;
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


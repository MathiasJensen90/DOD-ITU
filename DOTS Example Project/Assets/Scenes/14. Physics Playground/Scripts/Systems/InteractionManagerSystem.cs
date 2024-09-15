using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct InteractionManagerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InteractionConfig>();
    }
   
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;
        EntityCommandBuffer ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
        // EntityQuery allplayerQuery = GetEntityQuery(ComponentType.ReadOnly<PhysicsPlayer>());
        // NativeArray<Entity> allPlayerEntites = allplayerQuery.ToEntityArray(Allocator.TempJob);
        //var mainplayer = SystemAPI.GetSingletonEntity<PhysicsPlayer>();
        //var configComp = GetSingleton<InteractionConfig>();

        var physicsPlayer = SystemAPI.GetSingleton<PhysicsPlayer>();
        var allMoveObstacles = SystemAPI.GetComponentLookup<MoveObstacle>(true);

        var triggerObstacleJob = new TriggerObstacleJob
        {
            physicsPlayer = physicsPlayer,
            allMoveObstacles = allMoveObstacles,
            ecb = ecb
        }.Schedule(state.Dependency);
        
        var moveObstacleJob = new MoveObstacleJob
        {
            dt = dt
        }.Schedule(triggerObstacleJob);
        
        moveObstacleJob.Complete();
        ecb.Playback(state.EntityManager);
    }
}

public partial struct TriggerObstacleJob : IJobEntity
{
    public PhysicsPlayer physicsPlayer;
    [Unity.Collections.ReadOnly]
    public ComponentLookup<MoveObstacle> allMoveObstacles;
    public EntityCommandBuffer ecb; 

    public void Execute(in InteractionConfig config)
    {
        int playerPickupAmount = physicsPlayer.pickupAquirred;
        if (playerPickupAmount > config.interactionTreshold)
        {
            //var targetEntity = buffer[0].Value;
            Entity obstacleEntity = config.obstacle;
            if (!allMoveObstacles.HasComponent(obstacleEntity))
            {
                ecb.AddComponent(obstacleEntity, new MoveObstacle
                {
                    destinationValue = config.destinationOffset
                });
            }
        }
    }
}

public partial struct MoveObstacleJob : IJobEntity
{
    public float dt; 
    public void Execute(ref LocalTransform trans, ref MoveObstacle moveObstacle)
    {
        if (!moveObstacle.initLerp)
        {
            moveObstacle.initLerp = true;
            moveObstacle.destinationValue += trans.Position;
        }
        trans.Position = math.lerp(trans.Position, moveObstacle.destinationValue, dt * 1);;
    }
}
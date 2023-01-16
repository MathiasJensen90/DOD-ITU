using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// public partial class InteractionManagerSystem : SystemBase
// {
//     protected override void OnUpdate()
//     {
//         float dt = Time.DeltaTime;
//         EntityCommandBuffer ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);
//         // EntityQuery allplayerQuery = GetEntityQuery(ComponentType.ReadOnly<PhysicsPlayer>());
//         // NativeArray<Entity> allPlayerEntites = allplayerQuery.ToEntityArray(Allocator.TempJob);
//         var mainplayer = GetSingletonEntity<PhysicsPlayer>();
//         //var configComp = GetSingleton<InteractionConfig>();
//
//         var physicsPlayer = GetSingleton<PhysicsPlayer>();
//         var allMoveObstacles = GetComponentDataFromEntity<MoveObstacle>(true);
//
//         var triggerObstacleJob = new TriggerObstacleJob
//         {
//             physicsPlayer = physicsPlayer,
//             allMoveObstacles = allMoveObstacles,
//             ecb = ecb
//         }.Schedule();
//         
//         var moveObstacleJob = new MoveObstacleJob
//         {
//             dt = dt
//         }.Schedule(triggerObstacleJob);
//         
//         moveObstacleJob.Complete();
//         ecb.Playback(EntityManager);
//     }
// }
//
// public partial struct TriggerObstacleJob : IJobEntity
// {
//     public PhysicsPlayer physicsPlayer;
//     [Unity.Collections.ReadOnly]
//     public ComponentLookup<MoveObstacle> allMoveObstacles;
//     public EntityCommandBuffer ecb; 
//
//     public void Execute(DynamicBuffer<ObstacleBuffer> buffer , in InteractionConfig config)
//     {
//         int playerPickupAmount = physicsPlayer.pickupAquirred;
//         if (playerPickupAmount > config.interactionTreshold)
//         {
//             var targetEntity = buffer[0].Value;
//             if (!allMoveObstacles.HasComponent(targetEntity))
//             {
//                 Debug.Log("added movedobstacle");
//                 ecb.AddComponent(targetEntity, new MoveObstacle
//                 {
//                     destinationValue = config.obstacleDestination
//                 });
//             }
//         }
//     }
// }
//
// public partial struct MoveObstacleJob : IJobEntity
// {
//     public float dt; 
//     public void Execute(ref Translation trans, in MoveObstacle moveObstacle)
//     {
//         var destPos = math.lerp(trans.Value, moveObstacle.destinationValue, dt * 1);
//         trans.Value = destPos;
//     }
// }
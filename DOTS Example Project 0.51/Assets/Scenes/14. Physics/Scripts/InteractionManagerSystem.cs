using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class InteractionManagerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        
        EntityQuery allplayerQuery = GetEntityQuery(ComponentType.ReadOnly<PhysicsPlayer>());
        NativeArray<Entity> allPlayerEntites = allplayerQuery.ToEntityArray(Allocator.TempJob);

        ComponentDataFromEntity<PhysicsPlayer> allPhysicsPlayer = GetComponentDataFromEntity<PhysicsPlayer>(true);
        

        Entities.WithReadOnly(allPhysicsPlayer).ForEach((DynamicBuffer<ObstacleBuffer> buffer , in InteractionConfig config) =>
        {
            int playerPickupAmount = allPhysicsPlayer[allPlayerEntites[0]].pickupAquirred;
            
            if (playerPickupAmount > config.interactionTreshold)
            {
                var targetEntity = buffer[0].Value;
                if (!HasComponent<MoveObstacle>(targetEntity))
                {
                    Debug.Log("added movedobstacle");
                    
                    ecb.AddComponent(targetEntity, new MoveObstacle
                    {
                        destinationValue = config.obstacleDestination
                    });
                }
            }

        }).Schedule();
        
        Dependency.Complete();
        ecb.Playback(EntityManager);

        Entities.ForEach((ref Translation trans, in MoveObstacle moveObstacle) =>
        {
            var destPos = math.lerp(trans.Value, moveObstacle.destinationValue, dt * 1);
            trans.Value = destPos;
        }).Schedule();
    }
}

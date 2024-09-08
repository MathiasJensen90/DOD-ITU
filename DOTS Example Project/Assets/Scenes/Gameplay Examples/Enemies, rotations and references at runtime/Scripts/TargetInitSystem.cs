using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


public partial struct TargetInitSystem : ISystem
{
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameplayInteractionSingleton>();
    }
   
    public void OnUpdate(ref SystemState state)
    {
        var tower = SystemAPI.GetSingletonEntity<TowerTag>(); 
        var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

        foreach (var (transform, entity) in SystemAPI.Query<LocalTransform>().WithAll<ChaserTag>()
                     .WithNone<TowerTarget>()
                     .WithEntityAccess())
        {
            ecb.AddComponent(entity, new TowerTarget
            {
                Value = tower
            });
        }
        ecb.Playback(state.EntityManager);
    }
}

 



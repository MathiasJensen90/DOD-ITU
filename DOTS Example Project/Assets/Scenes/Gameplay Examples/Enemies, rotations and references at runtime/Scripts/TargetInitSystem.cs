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

        foreach (var (transform, entity) in SystemAPI.Query<LocalTransform>().WithAll<TowerTag>()
                     .WithNone<EnemyTargetBuffer>()
                     .WithEntityAccess())
        {
            Debug.Log("happened");
            ecb.AddBuffer<EnemyTargetBuffer>(entity);
        }
        
        // Entities.WithAll<TowerTag>().WithNone<EnemyTargetBuffer>().ForEach((Entity entity) =>
        // {
        //     EntityManager.AddBuffer<EnemyTargetBuffer>(entity);
        // }).WithStructuralChanges().Run();
        
        foreach (var (transform, entity) in SystemAPI.Query<LocalTransform>().WithAll<ChaserTag>()
                     .WithNone<TowerTarget>()
                     .WithEntityAccess())
        {
            ecb.AddComponent(entity, new TowerTarget
            {
                Value = tower
            });
            
            // var bufferFromTower = SystemAPI.GetBuffer<EnemyTargetBuffer>(tower); 
            // bufferFromTower.Add(new EnemyTargetBuffer
            // {
            //     Value = entity
            // });
        }
        
        
        // Entities.WithNone<TowerTarget>().WithAll<ChaserTag>().ForEach((Entity entity) =>
        // {
        //     ecb.AddComponent(entity, new TowerTarget
        //     {
        //         Value = tower
        //     });
        //
        //     var bufferFromTower = SystemAPI.GetBuffer<EnemyTargetBuffer>(tower); 
        //     bufferFromTower.Add(new EnemyTargetBuffer
        //     {
        //         Value = entity
        //     });
        // }).Schedule();
        
        //Dependency.Complete();
        ecb.Playback(state.EntityManager);
        //ecb.Dispose();
    }
}

    // public partial class TargetInitSystem1 : SystemBase
    // {
    //     protected override void OnCreate()
    //     {
    //         RequireForUpdate<GameplayInteractionSingleton>();
    //     }
    //
    //     protected override void OnUpdate()
    //     {
    //         var tower = SystemAPI.GetSingletonEntity<TowerTag>(); 
    //         var ecb = new EntityCommandBuffer(Allocator.TempJob);
    //         
    //         Entities.WithAll<TowerTag>().WithNone<EnemyTargetBuffer>().ForEach((Entity entity) =>
    //         {
    //             EntityManager.AddBuffer<EnemyTargetBuffer>(entity);
    //         }).WithStructuralChanges().Run();
    //
    //         Entities.WithNone<TowerTarget>().WithAll<ChaserTag>().ForEach((Entity entity) =>
    //         {
    //             ecb.AddComponent(entity, new TowerTarget
    //             {
    //                 Value = tower
    //             });
    //
    //             var bufferFromTower = SystemAPI.GetBuffer<EnemyTargetBuffer>(tower); 
    //             bufferFromTower.Add(new EnemyTargetBuffer
    //             {
    //                 Value = entity
    //             });
    //         }).Schedule();
    //
    //         Dependency.Complete();
    //         ecb.Playback(EntityManager);
    //         ecb.Dispose();
    //     }
    // }




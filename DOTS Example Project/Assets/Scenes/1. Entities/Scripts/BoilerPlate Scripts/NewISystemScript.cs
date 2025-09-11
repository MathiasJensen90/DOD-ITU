using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct NewISystemScript : ISystem
{
 
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state){
        
        // var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
        //     .CreateCommandBuffer(state.WorldUnmanaged);
        // foreach (var (transform, speedData, entity) in SystemAPI.Query<RefRW<LocalTransform>,
        //              RefRO<speedData>>().WithEntityAccess())
        // {  
        //     // valueRW (read and write)  
        //     // ValueRO (read only)
        //     ecb.RemoveComponent<speedData>(entity);
        //     transform.ValueRW.Position.x += speedData.ValueRO.speed;
        // }
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}


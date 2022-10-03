using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct InitSystem : ISystem 
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StateComp>();
    }

    public void OnDestroy(ref SystemState state)
    {
      
    }

    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<StateComp>();

        var size = config.size * config.size;
        var center = (config.size - 1) / 2f;

        for (int i = 0; i < size; i++)
        {
            float3 pos = float3.zero;
            pos.x = (i % config.size- center) * 1.5f;
            pos.z = (i / config.size - center) * 1.5f;
            var e = state.EntityManager.Instantiate(config.prefab);
            state.EntityManager.SetComponentData(e, new LocalToWorldTransform
            {
                Value = UniformScaleTransform.FromPosition(pos)
            });
        }
        state.Enabled = false;
    }
}

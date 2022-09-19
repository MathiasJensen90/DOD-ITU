using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



[DisableAutoCreation]
public partial class MoveForwardSystem : SystemBase 
{
    protected override void OnUpdate()
    {
        Entities.WithAll<bulletPrefab>().ForEach((ref Translation trans) =>
        {

            trans.Value += new float3(0, 1, 0);

        }).Schedule();
    }
}

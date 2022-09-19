using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnBulletsSystem : SystemBase
{
    protected override void OnUpdate()
    {

        var mousePos = (float3)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var input = Input.GetKey(KeyCode.Mouse0);
        
         Entities.ForEach((Entity enitty, in Translation trans, in bulletPrefab bulletPrefab) =>
         {
             if (input)
             {
                 var dir = math.normalize(mousePos.xy - trans.Value.xy);
                 var e = EntityManager.Instantiate(bulletPrefab.prefabTospawn);
                 EntityManager.SetComponentData(e, new Translation
                 {
                     Value = trans.Value + new float3(dir.x, dir.y, 0) * 3
                 });
                 var targetRot = quaternion.LookRotationSafe(new float3(dir.x, dir.y, 0), math.up());
                 EntityManager.SetComponentData(e, new Rotation
                 {
                     
                    Value = targetRot
                 });
             }
         }).WithStructuralChanges().Run();

    }
}

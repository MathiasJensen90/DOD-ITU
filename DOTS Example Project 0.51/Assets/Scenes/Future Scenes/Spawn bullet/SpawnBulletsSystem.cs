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
        Debug.Log(mousePos);
        
         Entities.ForEach((Entity enitty, in Translation trans, in WialiamTag wiliamTag) =>
         {
             if (Input.GetKeyDown(KeyCode.Mouse0))
             {
                 var dir = math.normalize(mousePos.xy - trans.Value.xy);
                 var e = EntityManager.Instantiate(wiliamTag.prefabTospawn);
                 EntityManager.SetComponentData(e, new Translation
                 {
                     Value = trans.Value + new float3(dir.x, dir.y, 0) * 3
                 });
        
             }
          
         }).WithStructuralChanges().Run();

    }
}

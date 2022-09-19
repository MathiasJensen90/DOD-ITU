using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class ApplyDamageSystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        var storage = GetStorageInfoFromEntity();
        var allTransforms = GetComponentDataFromEntity<Translation>(true);

        Entities.ForEach((ref DependencySingletonComponent singletonComp, in Translation trans) =>
        {
            #region EntitymangagerCheck
            
            // if (!EntityManager.Exists(singletonComp.targetEntity)) return;
            //
            // if (HasComponent<Translation>(singletonComp.targetEntity))
            //     {
            //          float3 targetEntityPos = GetComponent<Translation>(singletonComp.targetEntity).Value;
            //          //float3 targetEntityPos = allTransforms[singletonComp.targetEntity].Value; 
            //          singletonComp.distance = math.distance(trans.Value, targetEntityPos);
            //          
            //          //This is just for visualisation of range.
            //          Debug.DrawRay(trans.Value, math.normalizesafe(targetEntityPos) * singletonComp.radius, singletonComp.distance < singletonComp.radius ? Color.red : Color.magenta);
            //       
            //     }
            #endregion
            
            #region StorageInfoFromEntityCheck

            if (!storage.Exists(singletonComp.targetEntity)) return; 
            
                if (HasComponent<Translation>(singletonComp.targetEntity))
                {
                    var targetEntityPos = GetComponent<Translation>(singletonComp.targetEntity).Value;
                    singletonComp.distance = math.distance(trans.Value, targetEntityPos);
                    
                    Debug.DrawRay(trans.Value, targetEntityPos, singletonComp.distance < singletonComp.radius ? Color.red : Color.blue);
                    Debug.DrawRay(trans.Value, math.normalizesafe(targetEntityPos) * singletonComp.radius, singletonComp.distance < singletonComp.radius ? Color.red : Color.magenta);
                }
                
            #endregion

            #region CompondataFromEntityCheck
            
            // if (allTransforms.TryGetComponent(singletonComp.targetEntity,out Translation enemyTrans))
            // {
            //     enemyTrans = GetComponent<Translation>(singletonComp.targetEntity);
            //     singletonComp.distance = math.distance(trans.Value, enemyTrans.Value);
            // }
            #endregion
         
        }).WithoutBurst().Run();
        
        
        //make a IJobEntityVersion
        
        

        Entities.ForEach((Entity entity, in DependencySingletonComponent singletonComp) =>
        {
            var enemyEntity = singletonComp.targetEntity;
            if (!storage.Exists(enemyEntity)) return;
            
                if (singletonComp.distance < singletonComp.radius || Input.GetKeyDown(KeyCode.Space))
                {
                    if (!HasComponent<TookDamage>(enemyEntity))
                    {
                        Debug.Log("took damage");
                        EntityManager.AddComponentData(enemyEntity, new TookDamage
                        {
                            Value = singletonComp.damage
                        });
                    }
                }
        }).WithStructuralChanges().Run();

    }
}

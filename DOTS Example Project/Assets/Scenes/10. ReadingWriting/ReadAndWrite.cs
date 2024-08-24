using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class ReadWriteExample : SystemBase{
    protected override void OnUpdate()
    {
        
        Entities.ForEach((in LocalTransform trans, in ReadWriteComp readWriteComp) =>
        {
            Entity entityRef = readWriteComp.entityRef;
            float3 entityPos = SystemAPI.GetComponent<LocalTransform>(entityRef).Position; 

            EntityManager.SetComponentData(entityRef, new LocalTransform
            {
                Position = new float3(entityPos + new float3(0,readWriteComp.speed,0))
            });
        }).WithoutBurst().Run();
    }
}
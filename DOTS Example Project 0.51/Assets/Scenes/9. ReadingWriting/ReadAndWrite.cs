using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class ReadWriteExample : SystemBase{
    protected override void OnUpdate()
    {
        Entities.ForEach((in Translation trans, in ReadWriteComp readWriteComp) =>
        {
            Entity entityRef = readWriteComp.entityRef;
            float3 entityPos = GetComponent<Translation>(entityRef).Value;

            EntityManager.SetComponentData(entityRef, new Translation
            {
                Value = new float3(entityPos + new float3(0,readWriteComp.speed,0))
            });
        }).WithoutBurst().Run();
    }
}
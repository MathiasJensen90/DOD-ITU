using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class NativeContainersSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
       //entityQuery
       EntityQuery entityQuery = GetEntityQuery(ComponentType.ReadOnly<DiverseComponent>());
       NativeArray<Entity> allEntitieswithDivereComp = entityQuery.ToEntityArray(Allocator.TempJob);
       
       
       Entities.ForEach((Entity entity, ref SingletonComponent1 singletonComp) =>
       {
           var targetEntity = allEntitieswithDivereComp[0];
           var refTranslation = SystemAPI.GetComponent<LocalTransform>(targetEntity).Position;

           EntityManager.SetComponentData(targetEntity, new LocalTransform
           {
               Position = refTranslation
           });
           
       }).WithoutBurst().Run();
       
       allEntitieswithDivereComp.Dispose();
    }
}

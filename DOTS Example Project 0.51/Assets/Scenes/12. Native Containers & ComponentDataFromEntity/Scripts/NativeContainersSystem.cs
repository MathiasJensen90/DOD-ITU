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
        float dt = Time.DeltaTime;
        NativeArray<Entity> entityArray = new NativeArray<Entity>(5, Allocator.Temp);
        

        //componentdata from entity
       ComponentDataFromEntity<Translation> allTranslation = GetComponentDataFromEntity<Translation>();

       //entityQuery
       EntityQuery entityQuery = GetEntityQuery(ComponentType.ReadOnly<DiverseComponent>());
       NativeArray<Entity> allEntitieswithDivereComp = entityQuery.ToEntityArray(Allocator.TempJob);
        
       //using componentdata with entity as index to get specfic translation
       Translation translationFromEntity = allTranslation[allEntitieswithDivereComp[0]]; 
       
       Entities.ForEach((Entity entity, ref SingletonComponent1 singletonComp) =>
       {
           var targetEntity = allEntitieswithDivereComp[0];

          
           
           //  getcomponent is a wrapper that does the componentdatafromentity behind the scenes //
           
           var refTranslation = GetComponent<Translation>(targetEntity).Value;
           float3 targetTranslation = allTranslation[targetEntity].Value;
           
           

           var newTranslation = targetTranslation += new float3(0, 0, 1) * dt;
           
           EntityManager.SetComponentData(targetEntity, new Translation
           {
               Value = refTranslation
           });
           
       }).WithoutBurst().Run();

       //entityArray.Dispose();
       allEntitieswithDivereComp.Dispose();
    }
}

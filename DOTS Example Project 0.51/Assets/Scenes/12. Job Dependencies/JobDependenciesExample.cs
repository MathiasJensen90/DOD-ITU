using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[DisableAutoCreation]
public partial class JobDependenciesExample : SystemBase
{
    protected override void OnUpdate()
    {
        #region Example1
        var jobA = new ExampleJob();
        var jobB = new ExampleJob();
        var jobC = new ExampleJob();
        var jobD = new ExampleJob();
        
        JobHandle AHandle = jobA.Schedule();
        JobHandle BHandle = jobB.Schedule(AHandle);
        JobHandle CHandle = jobC.Schedule(BHandle);
        JobHandle DHandle = jobD.Schedule(CHandle);
        
        
        DHandle.Complete();
        

        #endregion
        
        #region Example2
        var jobE = new ExampleJob();
        var jobF = new ExampleJob();
        var jobG = new ExampleJob();
        var jobH = new ExampleJob();
        
        JobHandle EHandle = jobE.Schedule();
        JobHandle FHandle = jobF.Schedule();
        JobHandle GHandle = jobG.Schedule();
        
        JobHandle CombinedJobHandle = JobHandle.CombineDependencies(EHandle, FHandle, GHandle);
        
        JobHandle HHandle = jobH.Schedule(CombinedJobHandle);
        
        HHandle.Complete();

        #endregion

    }
}


public partial struct ExampleJob : IJobEntity
{
    public void Execute()
    {
        
    }
}

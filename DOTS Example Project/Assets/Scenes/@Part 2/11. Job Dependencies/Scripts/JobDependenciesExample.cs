using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;


public partial struct JobDependenciesExample : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<JobDepedencySingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        #region Chaining Job Dependencies
        
        /*
         In this example, we schedule four jobs (Job A, B, C, D)
          and each job depends on the previous one to complete before it can start.
        This is called chaining jobs.
        
        How It Works:

        Job A runs first because it is scheduled using state.Dependency (the current state).
        Job B depends on Job A. It will start only after A finishes (jobB.Schedule(AHandle)).
        Job C depends on Job B, and Job D depends on Job C.
        DHandle.Complete() ensures that all these jobs finish before moving forward. If the handle was not called,
        the job system would sort out the depedencies and complete the job later
         */
        ExampleJob jobA = new ExampleJob();
        ExampleJob jobB = new ExampleJob();
        ExampleJob jobC = new ExampleJob();
        ExampleJob jobD = new ExampleJob();
        
        JobHandle AHandle = jobA.Schedule(state.Dependency);
        JobHandle BHandle = jobB.Schedule(AHandle);
        JobHandle CHandle = jobC.Schedule(BHandle);
        JobHandle DHandle = jobD.Schedule(CHandle);
        
        //DHandle.Complete();
        state.Dependency = DHandle;
        #endregion
        
        
        #region Combining dependencies
        
        /*
        Sometimes, we have multiple jobs that don't depend on each other directly but need to complete before
        another job starts. In this case, we can use JobHandle.CombineDependencies() to combine their handles
        and wait for them all to finish.
         */
        ExampleJob jobE = new ExampleJob();
        ExampleJob jobF = new ExampleJob();
        ExampleJob jobG = new ExampleJob();
        ExampleJob jobH = new ExampleJob();
        
        JobHandle EHandle = jobE.Schedule(state.Dependency);
        JobHandle FHandle = jobF.Schedule(state.Dependency);
        JobHandle GHandle = jobG.Schedule(state.Dependency);
        
        JobHandle CombinedJobHandle = JobHandle.CombineDependencies(EHandle, FHandle, GHandle);
        
        //JobH will not run before the previous 3 jobs have finished
        JobHandle HHandle = jobH.Schedule(CombinedJobHandle);
        
        //HHandle.Complete();
        state.Dependency = HHandle;

        #endregion

    }
}


public partial struct ExampleJob : IJobEntity
{
    
    public void Execute()
    {
        
    }
}

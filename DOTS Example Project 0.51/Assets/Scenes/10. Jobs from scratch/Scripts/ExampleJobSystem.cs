using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;


public partial class ExampleJobSystems : SystemBase
{
    protected override void OnCreate()
    {
       RequireSingletonForUpdate<IJobSingleton>();
    }

    protected override void OnUpdate()
    {
        Enabled = false;

        NativeArray<float> nums = new NativeArray<float>(5, Allocator.TempJob);
        
        myIJob firstJob = new myIJob
        {
            numbers = nums
        };
        AnotherJob secondJob = new AnotherJob
        {
            numbers = nums,
        };
        
        
        JobHandle handle = firstJob.Schedule();
        JobHandle handle2 = secondJob.Schedule(handle);

        handle2.Complete();

        Debug.Log(nums[0]);

        nums.Dispose();
    }
}


public struct myIJob : IJob
{
    public NativeArray<float> numbers;
    
    public void Execute()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }
    }
}

[BurstCompile]
public struct AnotherJob : IJob
{
    public NativeArray<float> numbers;

    public void Execute()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[0] += numbers[i];
        }
    }
}




//Ijob
// Dependencies and complex computations
//IjobFor
//
//IJobEntityBatch
//IJobEntityBatchWithIndex
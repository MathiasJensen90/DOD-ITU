using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[DisableAutoCreation]
public partial struct IJobExampleSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
       state.RequireForUpdate<enemyInteractionTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        NativeArray<float> nums = new NativeArray<float>(5, Allocator.TempJob);
        
        JobHandle firstJob = new myIJob
        {
            numbers = nums
        }.Schedule(state.Dependency);
        JobHandle secondJob = new AnotherJob
        {
            numbers = nums,
        }.Schedule(firstJob);
        
        secondJob.Complete();

         for (int i = 0; i < nums.Length; i++)
         {
             Debug.Log($"{nums[i]}");
         }
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
            numbers[i] = i + 1;
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

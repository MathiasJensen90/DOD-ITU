using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public partial struct DepedencyExercise1 : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<JobDepedencySingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //the system only runs once after initialized
        state.Enabled = false;

        var sharedArray = new NativeArray<int>(10, Allocator.TempJob);
        
        JobHandle incrementJobHandle = state.Dependency = new incrementJob
        {
            array = sharedArray,
            valueToSet = 3
        }.Schedule(state.Dependency);

        var secondIncrementJobHandle = new incrementJob
        {
            array = sharedArray,
            valueToSet = 2
        }.Schedule(incrementJobHandle);
        
        secondIncrementJobHandle.Complete();
        
        for (int i = 0; i < sharedArray.Length; i++)
        {
            Debug.Log($"Index {i}: {sharedArray[i]}");
        }
        sharedArray.Dispose();
    }
}


    [BurstCompile]
    public partial struct incrementJob : IJobEntity
    {
        public NativeArray<int> array;
        public int valueToSet;

        public void Execute()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = valueToSet;
            }
        }
    }


using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public partial struct DepedencyExercise1 : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<JobDependencySingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //the system only runs once after initialized
        state.Enabled = false;

        var sharedArray = new NativeArray<int>(10, Allocator.TempJob);
        
        JobHandle incrementJobHandle  = new IncrementJob
        {
            array = sharedArray,
            valueToSet = 7
        }.Schedule(state.Dependency);
        
        state.Dependency = incrementJobHandle;

        var secondIncrementJobHandle = new IncrementJob
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
    public partial struct IncrementJob : IJobEntity
    {
        public NativeArray<int> array;
        public int valueToSet;

        public void Execute(in JobDependencySingleton singleton)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = valueToSet;
            }
        }
    }


using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

public class JobSafetyExample : MonoBehaviour
{
    private NativeArray<int> sharedArray;

    private void Start()
    {
        // Initialize the NativeArray with 10 elements
        sharedArray = new NativeArray<int>(10, Allocator.TempJob);
        
        var job1 = new WriteJob
        {
            array = sharedArray,
        }.Schedule();  

        var job2 = new WriteJob
        {
            array = sharedArray,
        }.Schedule();
        
        sharedArray.Dispose();
    }

    // Simple job that writes values to the array
    [BurstCompile]
    private struct WriteJob : IJob
    {
        //[NativeDisableContainerSafetyRestriction]
        public NativeArray<int> array;
        
        public void Execute()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }
    }
}
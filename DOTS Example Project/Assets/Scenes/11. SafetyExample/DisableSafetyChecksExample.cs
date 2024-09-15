using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

public class DisableSafetyChecksExample : MonoBehaviour
{
    // Define the NativeArray that will be shared between jobs
    private NativeArray<int> sharedArray;

    private void Start()
    {
        // Initialize the NativeArray with 10 elements
        sharedArray = new NativeArray<int>(10, Allocator.TempJob);

        // Schedule two jobs that modify different slices of the array
        var job1 = new SliceJob
        {
            // First 5 elements of the array (indices 0 to 4)
            array = sharedArray,
            startIndex = 0,
            length = 5
        }.Schedule();

        var job2 = new SliceJob
        {
            // Next 5 elements of the array (indices 5 to 9)
            array = sharedArray,
            startIndex = 5,
            length = 5
        }.Schedule(job1);
        

        // Print the array to show that both slices were updated without conflict
        for (int i = 0; i < sharedArray.Length; i++)
        {
            Debug.Log($"Index {i}: {sharedArray[i]}");
        }
        
        sharedArray.Dispose();
    }

    [BurstCompile]
    private struct SliceJob : IJob
    {
        [NativeDisableContainerSafetyRestriction]
        public NativeArray<int> array;

        public int startIndex;
        public int length;

        public void Execute()
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                array[i] = i;
            }
        }
    }
}

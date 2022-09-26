using System.ComponentModel;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class FindNearestSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery query = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<TargetData>());
        var targetPositions = query.ToComponentDataArray<Translation>(World.UpdateAllocator.ToAllocator);
        var configFil = GetSingleton<InitData>();

        if (configFil.config == SchedulingConfig.schedule)
        {
            var findNearestJob = new FindNearestJob
            {
                transArray = targetPositions
            }.Schedule();
            findNearestJob.Complete();
        }
        else if(configFil.config == SchedulingConfig.ScheduleParallel)
        {
            var findNearestJob = new FindNearestJob
            {
                transArray = targetPositions
            }.ScheduleParallel();
            findNearestJob.Complete();
        }
        else if(configFil.config == SchedulingConfig.ScheduleBursted)
        {
            var findNearestJob = new BurstedFindNearestJob
            {
                transArray = targetPositions
            }.Schedule();
            findNearestJob.Complete();
        }
        else if(configFil.config == SchedulingConfig.ScheduleParallelBursted)
        {
            var findNearestJob = new BurstedFindNearestJob
            {
                transArray = targetPositions
            }.ScheduleParallel();
            findNearestJob.Complete();
        }
        else if(configFil.config == SchedulingConfig.sortedArray)
        {
            SortJob<Translation, AxisComparer> sortJob = targetPositions.SortJob(new AxisComparer());

            var sortJob2 = targetPositions.SortJob(new AxisComparer()).Schedule();
            var findNearestJob = new SortedFindingNearest
            {
                transArray = targetPositions
            }.ScheduleParallel(sortJob2);
            
            findNearestJob.Complete();
        }
    }
}


public partial struct FindNearestJob : IJobEntity
{
    [Unity.Collections.ReadOnly]
    public NativeArray<Translation> transArray;
    
    public void Execute(ref SeekerData seeker,  in Translation trans)
    {
        
        float3 currentPos = trans.Value;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < transArray.Length; i++)
        {
            float3 targetPos = transArray[i].Value;
            float dist = math.distancesq(currentPos, targetPos);
            if (dist < nearestDist)
            {
                nearestDist = dist; 
                seeker.neareestTarget = targetPos;
            }
        }
        Debug.DrawLine(currentPos, seeker.neareestTarget);
    }
}

[BurstCompile]
public partial struct BurstedFindNearestJob : IJobEntity
{
    [Unity.Collections.ReadOnly]
    public NativeArray<Translation> transArray;
    
    public void Execute(ref SeekerData seeker,  in Translation trans)
    {
        
        float3 currentPos = trans.Value;
        float nearestDist = float.MaxValue;
        for (int i = 0; i < transArray.Length; i++)
        {
            float3 targetPos = transArray[i].Value;
            float dist = math.distancesq(currentPos, targetPos);
            if (dist < nearestDist)
            {
                nearestDist = dist; 
                seeker.neareestTarget = targetPos;
            }
        }
        Debug.DrawLine(currentPos, seeker.neareestTarget);
    }
}

[BurstCompile]
public partial struct SortedFindingNearest : IJobEntity
{
    [Unity.Collections.ReadOnly]
    public NativeArray<Translation> transArray;

    public void Execute(ref SeekerData seeker,  in Translation trans)
    {
        float3 seekerPos = trans.Value;

        // Find the target with the closest X coord.
        int startIdx = transArray.BinarySearch(trans, new AxisComparer() { });

        // When no precise match is found, BinarySearch returns the bitwise negation of the last-searched offset.
        // So when startIdx is negative, we flip the bits again, but we then must ensure the index is within bounds. 
        if (startIdx < 0) startIdx = ~startIdx;
        if (startIdx >= transArray.Length) startIdx = transArray.Length - 1;

        // The position of the target with the closest X coord.
        float3 nearestTargetPos = transArray[startIdx].Value;
        float nearestDistSq = math.distancesq(seekerPos, nearestTargetPos);

        // Searching upwards through the array for a closer target. 
        Search(seekerPos, startIdx + 1, transArray.Length, +1, ref nearestTargetPos, ref nearestDistSq);

        // Search downwards through the array for a closer target.
        Search(seekerPos, startIdx - 1, -1, -1, ref nearestTargetPos, ref nearestDistSq);

        seeker.neareestTarget = nearestTargetPos;
    
    Debug.DrawLine(seekerPos, seeker.neareestTarget);
    }
    
    void Search(float3 seekerPos, int startIdx, int endIdx, int step,
        ref float3 nearestTargetPos, ref float nearestDistSq)
    {
        for (int i = startIdx; i != endIdx; i += step)
        {
            float3 targetPos = transArray[i].Value;
            float xdiff = seekerPos.x - targetPos.x;

            // If the square of the x distance is greater than the current nearest, we can stop searching. 
            if ((xdiff * xdiff) > nearestDistSq) break;

            float distSq = math.distancesq(targetPos, seekerPos);

            if (distSq < nearestDistSq)
            {
                nearestDistSq = distSq;
                nearestTargetPos = targetPos;
            }
        }
    }
}
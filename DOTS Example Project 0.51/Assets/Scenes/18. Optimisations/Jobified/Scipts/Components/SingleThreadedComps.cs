using System;using Unity.Entities;
using Unity.Mathematics;

public struct InitData : IComponentData
{
    public int NumSeekers;
    public int NumTargets;
    public float2 bounds;
    public Entity SeekerPrefab;
    public Entity TargetPrefab;
    public SchedulingConfig config;
}


 public enum SchedulingConfig{
     schedule,
     ScheduleParallel,
     ScheduleBursted,
     ScheduleParallelBursted,
     sortedArray
 }


using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct ECBSingletonComponent : IComponentData
{
    public SchedulingType SchedulingType;
    public int spawnAmount;
    public Entity prefabTospawn;
}


public enum SchedulingType{
    Run,
    Schedule,
    ScheduleParallel
}
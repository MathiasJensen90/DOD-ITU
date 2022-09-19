using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public partial class IjobEntityBatchExample : SystemBase
{
    
     EntityQuery m_Query;

    protected override void OnCreate()
    {
        // Cached access to a set of ComponentData based on a specific query
        m_Query = GetEntityQuery(typeof(Rotation), ComponentType.ReadOnly<JobentityData>());
    }
    
    // Use the [BurstCompile] attribute to compile a job with Burst.
    [BurstCompile]
    struct RotationSpeedJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<Rotation> RotationTypeHandle;
        [Unity.Collections.ReadOnly] public ComponentTypeHandle<JobentityData> RotationSpeedTypeHandle;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            var chunkRotations = batchInChunk.GetNativeArray(RotationTypeHandle);
            var chunkRotationSpeeds = batchInChunk.GetNativeArray(RotationSpeedTypeHandle);
            for (var i = 0; i < batchInChunk.Count; i++)
            {
                var rotation = chunkRotations[i];
                var rotationSpeed = chunkRotationSpeeds[i];

                // Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
                chunkRotations[i] = new Rotation
                {
                    Value = math.mul(math.normalize(rotation.Value),
                        quaternion.AxisAngle(math.up(), math.radians(rotationSpeed.rotationValue) * DeltaTime))
                };
            }
        }
    }
    
    

    // OnUpdate runs on the main thread.
    protected override void OnUpdate()
    {
        // // Explicitly declare:
        // // - Read-Write access to Rotation
        // // - Read-Only access to RotationSpeed_IJobChunk
        var rotationType = GetComponentTypeHandle<Rotation>();
        var rotationSpeedType = GetComponentTypeHandle<JobentityData>(true);
        float deltaTime = Time.DeltaTime;
        
        var job = new RotationSpeedJob()
        {
            RotationTypeHandle = rotationType,
            RotationSpeedTypeHandle = rotationSpeedType,
            DeltaTime = deltaTime
        };
        
        Dependency = job.ScheduleParallel(m_Query, Dependency);

        //all the above is autogenereated with below entities.foreach
        #region entities.foreach

        // Entities.ForEach((Entity entity, ref Rotation rot, in JobentityData jobentityData) =>
        // {
        //     rot.Value = math.mul(math.normalize(rot.Value),
        //         quaternion.AxisAngle(math.up(), math.radians(jobentityData.rotationValue) * deltaTime));
        // }).ScheduleParallel();

        #endregion

  
    }
    
    
}


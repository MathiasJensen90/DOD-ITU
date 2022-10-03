using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisableAutoCreation]
[UpdateInGroup(typeof(GameObjectAfterConversionGroup))]
public class ConstructingBlobAsset : GameObjectConversionSystem
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<BlobSingleton>();
    }

    protected override void OnUpdate()
    {

        BlobAssetReference<WaypointBlobAsset> waypointBlobAssetReference; 
        
        //initiliase
        BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp);
        ref WaypointBlobAsset waypointBlobAsset = ref blobBuilder.ConstructRoot<WaypointBlobAsset>();
        BlobBuilderArray<Waypoint> waypointArray = blobBuilder.Allocate(ref waypointBlobAsset.waypointArray, 3);
        
        //populate with data
        waypointArray[0] = new Waypoint
        {
            positon = new float3(1, 5, 1),
            waypontName = "Scotland"
        };
        waypointArray[1] = new Waypoint
        {
            positon = new float3(2, 2, 2),
            waypontName = "Netherlands"
        };

        waypointArray[2] = new Waypoint
        {
            positon = new float3(11, 11, 11),
            waypontName = "Denmark"
        };

        waypointBlobAssetReference = blobBuilder.CreateBlobAssetReference<WaypointBlobAsset>(Allocator.Persistent);

        EntityQuery singletonQuery = DstEntityManager.CreateEntityQuery(typeof(BlobSingleton));
        Entity entity = singletonQuery.GetSingletonEntity();
        //var singletonEntity = GetSingletonEntity<BlobSingleton>();

        DstEntityManager.AddComponentData(entity, new WaypointComponent
        {
            waypointBlobAssetRef = waypointBlobAssetReference
        });

        blobBuilder.Dispose();
    }
}


public struct Waypoint
{
    public float3 positon;
    public FixedString32Bytes waypontName;
}

public struct WaypointBlobAsset
{
    public BlobArray<Waypoint> waypointArray;
}

public struct WaypointComponent : IComponentData
{
    public BlobAssetReference<WaypointBlobAsset> waypointBlobAssetRef;
}
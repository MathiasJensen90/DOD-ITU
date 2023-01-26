using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class QuerySingletonAuthoring : MonoBehaviour
{
    class Baker : Baker<QuerySingletonAuthoring>
    {
        public override void Bake(QuerySingletonAuthoring authoring)
        {
            AddComponent<QuerySingleton>();
        }
    }
}

public struct QuerySingleton : IComponentData
{
    
}

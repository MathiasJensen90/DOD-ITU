using Unity.Entities;
using UnityEngine;

public class QuerySingletonBaker : MonoBehaviour
{

    class baker : Baker<QuerySingletonBaker>
    {
        public override void Bake(QuerySingletonBaker authoring)
        {
            AddComponent<QuerySingleton>();

        }
    }
}

public struct QuerySingleton : IComponentData
{
        
}
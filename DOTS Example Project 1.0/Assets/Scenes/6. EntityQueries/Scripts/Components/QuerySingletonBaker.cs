using Unity.Entities;
using UnityEngine;

public class QuerySingletonBaker : MonoBehaviour
{

    class baker : Baker<QuerySingletonBaker>
    {
        public override void Bake(QuerySingletonBaker authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<QuerySingleton>(entity);
        }
    }
}

public struct QuerySingleton : IComponentData
{
        
}
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class PrefabAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject prefab;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

        dstManager.AddComponentData(entity, new EntityPrefabHolder
        {
            Value = conversionSystem.GetPrimaryEntity(prefab)
        });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
     {
         referencedPrefabs.Add(prefab);
     }
 }

public struct EntityPrefabHolder : IComponentData
{
    public Entity Value; 
}

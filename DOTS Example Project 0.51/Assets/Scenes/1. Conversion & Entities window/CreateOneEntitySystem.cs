using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public partial class CreateOneEntitySystem : SystemBase
{
    protected override void OnCreate()
    {
        //Our system will only run if this singletonComponent is present in the world. This can be a good way of bootstraping a scene.
        RequireSingletonForUpdate<CreateEntityComponent>();
    }

    protected override void OnUpdate()
    {
        //disabling this makes the system run only once.
        Enabled = false;
        
        //you cannot nul check like in classic unity. entity != null will not work!. You have to do it this way
        var entity = Entity.Null;
        
        // A method useful for getting a singleton component (only one of these components exist in the world)
        var createEntityComp = GetSingleton<CreateEntityComponent>();

        if (createEntityComp.createEntityWithArchetype)
        {
            // Every entity has an EntityArchetype (it's specific combination of components), whether we assign it explicitly here or not.
            //also remember that entities archetype can change at runtime depending on added/removed components 
            EntityArchetype archetype = EntityManager.CreateArchetype(
                typeof(Translation),
                typeof(Rotation),
                typeof(LocalToWorld)
            );
            entity = EntityManager.CreateEntity(archetype);
        }
        else
        {
            entity = EntityManager.CreateEntity();
            //note that if we just used  AddComponentData here, it would be sufficient. The method will add the component if it is not already there.
            //showing both methods here to be explicit
            EntityManager.AddComponent<Translation>(entity);
            // an entity can only have one component of a type. Adding the same component again will simply not do anything.
            EntityManager.AddComponent<Translation>(entity);
            EntityManager.AddComponentData(entity, new Translation {Value = createEntityComp.position});
        }
        
        EntityManager.SetName(entity, "MySpecialEntity");
    }
}

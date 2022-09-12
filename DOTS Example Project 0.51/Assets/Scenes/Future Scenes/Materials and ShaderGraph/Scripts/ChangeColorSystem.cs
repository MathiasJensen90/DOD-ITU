using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public partial class ChangeColorSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        float elapsedTime = (float)Time.ElapsedTime;
        
        Entities.ForEach((Entity entity, ref  MyMaterial color, in DestinationColor destColor, in PlayerInputComponent input) => {

            if (input.input1Value)
            {
                color.Value = new float4(.5f, .5f, 0, 0);
            }

            var colorLerp = math.lerp(color.Value, destColor.Value, dt);
            color.Value = colorLerp; 
        }).Schedule();


        Entities.ForEach((ref DisolveComp disolveComp) =>
        {

            var disolveLerp = math.remap(-1, 1, 0, 1, math.sin(elapsedTime));
 
            disolveComp.Value = disolveLerp;
        }).Schedule();
    }
}

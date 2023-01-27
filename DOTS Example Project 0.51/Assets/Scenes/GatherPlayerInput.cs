using Unity.Entities;
using UnityEngine;

public partial class GatherPlayerInput : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref PlayerInputComponent inputComp, in PlayerButtonsComponent playerButtons) =>
        {
            inputComp.input1Value = Input.GetKeyDown(playerButtons.input1); 
            inputComp.input2Value = Input.GetKeyDown(playerButtons.input2); 
            inputComp.input3Value = Input.GetKeyDown(playerButtons.input3); 
            inputComp.input4Value = Input.GetKeyDown(playerButtons.input4); 
            inputComp.input5Value = Input.GetKeyDown(playerButtons.input5); 
            inputComp.input6Value = Input.GetKeyDown(playerButtons.input6); 
        }).Run();
    }
}

using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputAuthoring : MonoBehaviour
{
   public KeyCode input1;
   public KeyCode input2;
   public KeyCode input3;
   public KeyCode input4;
   public KeyCode input5;
   public KeyCode input6;
   
   public void Convert(Entity entity, EntityManager dstManager)
   {
      dstManager.AddComponentData(entity, new PlayerButtonsComponent
      {
         input1 = input1,
         input2 = input2,
         input3 = input3,
         input4 = input4,
         input5 = input5,
         input6 = input6
      });
      dstManager.AddComponent<PlayerInputComponent>(entity);
   }
}


public struct PlayerInputComponent : IComponentData
{
   public bool input1Value;
   public bool input2Value;
   public bool input3Value;
   public bool input4Value;
   public bool input5Value;
   public bool input6Value;
}

public struct PlayerButtonsComponent : IComponentData
{
   public KeyCode input1;
   public KeyCode input2;
   public KeyCode input3;
   public KeyCode input4;
   public KeyCode input5;
   public KeyCode input6;
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public struct AxisComparer : IComparer<LocalTransform> 
{
    public int Compare(LocalTransform a, LocalTransform b)
    {
        return a.Position.x.CompareTo(b.Position.x);
    }
}

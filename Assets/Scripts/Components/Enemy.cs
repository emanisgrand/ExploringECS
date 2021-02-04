using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct Enemy : IComponentData
{
    public float3 previousCell;
}

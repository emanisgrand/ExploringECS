using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        Entities
            .WithAll<Player>()
            .ForEach((ref Movable movable) =>
            {
                movable.direction = new float3(x, 0, y);
            }).Schedule();
    }
}

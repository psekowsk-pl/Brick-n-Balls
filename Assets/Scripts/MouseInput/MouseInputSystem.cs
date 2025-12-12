using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MouseInputSystem : ISystem
{
    /*public void OnUpdate(ref SystemState state)
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Camera cam = Camera.main;

        float3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);

        foreach (var (ball, transform, entity) in SystemAPI.Query<RefRW<BallComponent>, RefRW<LocalTransform>>().WithEntityAccess())
        {
            if (ball.ValueRO.IsFired) continue;

            float3 dir = math.normalize(mouseWorld - transform.ValueRO.Position);

            ball.ValueRW.IsFired = true;
            ball.ValueRW.MouseInput = new float2(dir.x, dir.y);
        }
    }*/
}

using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Physics;

public partial struct BallSystem : ISystem
{
    private EntityManager EntityManager;
    private NativeArray<Entity> Entities;

    private void OnUpdate(ref SystemState state)
    {
        EntityManager = state.EntityManager;

        Entities = EntityManager.GetAllEntities(Allocator.Temp);

        foreach (var (ball, physicsVelocity, entity) in SystemAPI.Query<RefRW<BallComponent>, RefRW<PhysicsVelocity>>().WithEntityAccess())
        {
            if (!ball.ValueRO.IsFired) return;

            ball.ValueRW.Velocity = new float3(ball.ValueRO.MouseInput.x * ball.ValueRO.MoveSpeed * SystemAPI.Time.DeltaTime, ball.ValueRO.MouseInput.y * ball.ValueRO.MoveSpeed * SystemAPI.Time.DeltaTime, 0f);

            physicsVelocity.ValueRW.Linear = ball.ValueRW.Velocity;
        }

        Entities.Dispose();
    }
}
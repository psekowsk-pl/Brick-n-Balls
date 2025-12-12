using Unity.Entities;
using Unity.Mathematics;

public struct BallComponent : IComponentData
{
    public float3 Velocity;
    public float2 MouseInput;
    public float MoveSpeed;
    public bool IsFired;
}
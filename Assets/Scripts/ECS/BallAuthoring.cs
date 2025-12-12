using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BallAuthoring : MonoBehaviour
{
    public float MoveSpeed = 2000f;

    public class BallAuthoringBaker : Baker<BallAuthoring>
    {
        public override void Bake(BallAuthoring authoring)
        {
            Entity ballAuthoring = GetEntity(TransformUsageFlags.None);

            AddComponent(ballAuthoring, new BallComponent { 
                Velocity = Vector3.zero,
                MouseInput = float2.zero,
                MoveSpeed = authoring.MoveSpeed,
                IsFired = false
            });
        }
    }
}
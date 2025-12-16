using Unity.Entities;
using UnityEngine;

public struct ScoreData : IComponentData
{
    public int value;
}

public class ScoreAuthoring : MonoBehaviour
{
    public class Baker : Baker<ScoreAuthoring>
    {
        public override void Bake(ScoreAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ScoreData { value = 0 });
        }
    }
}
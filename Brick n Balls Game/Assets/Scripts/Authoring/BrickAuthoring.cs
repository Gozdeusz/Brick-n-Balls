using Unity.Entities;
using UnityEngine;

public struct BrickData : IComponentData
{
    public int hp;
}

public struct BrickTag : IComponentData { }

public class BrickAuthoring : MonoBehaviour
{
    [Range(1, 3)]
    public int hp = 1;
}

public class BrickBaker : Baker<BrickAuthoring>
{
    public override void Bake(BrickAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new BrickData
        {
            hp = authoring.hp
        });

        AddComponent<BrickTag>(entity);
    }
}
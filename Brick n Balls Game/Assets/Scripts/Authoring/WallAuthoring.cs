using Unity.Entities;
using UnityEngine;

public struct WallTag : IComponentData { }

public class WallAuthoring : MonoBehaviour
{
}

public class WallBaker : Baker<WallAuthoring>
{
    public override void Bake(WallAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);

        AddComponent<WallTag>(entity);
    }
}

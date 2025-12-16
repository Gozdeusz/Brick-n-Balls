using Unity.Entities;
using UnityEngine;

public class GameStateAuthoring : MonoBehaviour
{
    public class Baker : Baker<GameStateAuthoring>
    {
        public override void Bake(GameStateAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new GameStateData
            {
                state = GameState.Menu
            });
        }
    }
}

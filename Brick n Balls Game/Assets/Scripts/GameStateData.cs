using Unity.Entities;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public struct GameStateData : IComponentData
{
    public GameState state;
}

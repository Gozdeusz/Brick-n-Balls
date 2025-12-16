using Unity.Entities;

public partial struct GameOverSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var gameState = SystemAPI.GetSingletonRW<GameStateData>();

        if (gameState.ValueRO.state != GameState.Playing)
            return;

        bool anyBall = false;

        foreach (var _ in SystemAPI.Query<BallTag>())
        {
            anyBall = true;
            break;
        }

        if (!anyBall)
        {
            gameState.ValueRW.state = GameState.GameOver;
        }
    }
}

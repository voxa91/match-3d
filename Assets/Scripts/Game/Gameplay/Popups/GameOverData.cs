using MVC.Model;

namespace Game.Gameplay.Popups
{
    public enum GameResult
    {
        Lose,
        Win
    }

    public class GameOverData : IModel
    {
        public GameResult Result { get; private set; }

        public GameOverData(GameResult result)
        {
            Result = result;
        }
    }
}
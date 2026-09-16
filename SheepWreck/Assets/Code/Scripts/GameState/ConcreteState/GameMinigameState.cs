using UnityEngine;

public abstract class GameMinigameState : GameState
{
    public GameMinigameState(GameManager gameManager)
         : base(gameManager)
    {

    }


    public virtual void Win() { }
    
    public virtual void Loose() { }
}

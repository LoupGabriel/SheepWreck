using UnityEngine;

public abstract class GameState 
{

    protected GameManager gameManager;
    public GameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}

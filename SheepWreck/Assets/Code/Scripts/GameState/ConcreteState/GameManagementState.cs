using UnityEngine;

public class GameManagementState : GameState
{
    public GameManagementState(GameManager gameManager)
        : base(gameManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Management Mode");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Managment state");
    }
}

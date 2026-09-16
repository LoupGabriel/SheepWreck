using UnityEngine;

public class SheepwreckState : GameMinigameState
{
    public SheepwreckState(GameManager gameManager) : base(gameManager)
    {
    }
    private float m_elapse = 0;
    public override void Enter()
    {
        base.Enter();
        gameManager.SheepWreckController.EnableRope();
    }

    public override void Exit()
    {
        base.Exit();
        gameManager.SheepWreckController.ExitState();
    }
    public override void Update()
    {
        base.Update();
        GameManager.Instance.SheepWreckController.SetRope();

        m_elapse += Time.deltaTime;
        if (m_elapse >= gameManager.FireEventController.Duration)
        {
            Win();
            m_elapse = 0;
        }
    }

    public override void Win()
    {
        base.Win();
        //reward
        gameManager.ChangeState(new GameManagementState(gameManager));
    }

    public override void Loose()
    {
        base.Loose();
        //reward
        gameManager.ChangeState(new GameManagementState(gameManager));
    }
}

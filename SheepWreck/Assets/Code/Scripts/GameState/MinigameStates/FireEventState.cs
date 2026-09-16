using UnityEngine;

public class FireEventState : GameMinigameState
{
    private float m_elapse = 0;
    public FireEventState(GameManager gameManager) : base(gameManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        gameManager.FireEventController.SpawnFire();
        SoundtrackManager.Instance.PlayMusic("Fire");
    }

    public override void Update()
    {
        
        Debug.Log("fire");
        m_elapse += Time.deltaTime;
        if (m_elapse >= gameManager.FireEventController.Duration)
        {
            if (gameManager.FireEventController.AllFire.Count > 0)
            {
                Loose();
            }
            else
            {
                Win();
            }
            m_elapse = 0;
        }

    }
    public override void Exit()
    {
        base.Exit();
        foreach(FireObject fire in gameManager.FireEventController.AllFire)
        {
            fire.DestroyFire();
        }
        gameManager.FireEventController.AllFire.Clear();
        SoundtrackManager.Instance.PlayMusic("MainMusic");

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

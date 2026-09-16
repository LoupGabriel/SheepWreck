using UnityEngine;

public class SheepRescueState : GameMinigameState 
{


    private GameObject m_spawnSheep;
    public SheepRescueState(GameManager gameManager) : base(gameManager)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        m_spawnSheep = gameManager.SheepRescueController.SpawnRescueSheep();
    }
    public override void Update()
    {
        base.Update();
        gameManager.SheepRescueController.RescueMovement(m_spawnSheep);
        if (m_spawnSheep.transform.position.x > 70)
        {
            Loose();
        }
    }
   

    public override void Win()
    {
        base.Win();
        Debug.Log("Sheep Rescued");
        gameManager.SheepRescueController.DestroyIsland(m_spawnSheep.gameObject);
        gameManager.ChangeState(new GameManagementState(gameManager));
    }
    public override void Loose()
    {
        base.Loose();
        Debug.Log("SheepLost");
        gameManager.SheepRescueController.DestroyIsland(m_spawnSheep.gameObject);
        gameManager.ChangeState(new GameManagementState(gameManager));

    }



}

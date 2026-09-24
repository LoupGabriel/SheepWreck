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
        foreach(RoomInstance room in ShipSystem.Instance.m_shipCurrentRooms)
        {
            BoxCollider boxCollider = room.GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }
    }

    public override void Update()
    {
        
        Debug.Log("fire");
        m_elapse += Time.deltaTime;
        if(gameManager.FireEventController.AllFire.Count <= 0)
        {
            Win();
        }
        if (m_elapse >= gameManager.FireEventController.Duration)
        {
            if (gameManager.FireEventController.AllFire.Count > 0)
            {
                Loose();
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
        foreach(Transform t in gameManager.FireEventController.SpawnPoint)
        {
            gameManager.FireEventController.SpawnPoint.Remove(t);
        }
        gameManager.FireEventController.AllFire.Clear();
        SoundtrackManager.Instance.PlayMusic("SetSail");
        foreach (RoomInstance room in ShipSystem.Instance.m_shipCurrentRooms)
        {
            BoxCollider boxCollider = room.GetComponent<BoxCollider>();
            boxCollider.enabled = true;
        }

    }
    
    public override void Win()
    {
        base.Win();
        SfxManager.PlaySfx("Upgrade02");
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


using UnityEngine;

public class SheepInstance : MonoBehaviour
{


    
  
    
    [SerializeField] private SheepNamesDataBase m_sheepNames;
    [SerializeField] public string m_sheepName;

    [SerializeField] private float m_hungerTimer = 5f;
    [SerializeField] private float m_thirstTimer = 2f;
    private Animator m_animator;
    #region State Machine Variables
    public SheepStateMachine m_stateMachine { get; set; }
    public SheepIdleState m_idleState { get; set; }
    public SheepWorkingState m_workinState { get; set; }
    public SheepWaitForWorkState m_waitForWorkState { get; set; }
    public SheepRestingState m_restingState { get; set; }
    public SheepEatingState m_eatingState { get; set; }

    #endregion

    public float m_productionRate = 1;
    public SpriteRenderer m_spriteRenderer;
    public int m_currentHp;
    public int m_MaxHp;

    public int level;
   
    

    //ressource
    public int m_currentMorale;

    public int m_hunger;
    public int m_thirst;

    public RoomInstance m_assignedRoom;
    private float m_hungerElapse = 0;
    private float m_thirstElapse = 0;

    
    public bool IsWorking => m_stateMachine != null && m_stateMachine.m_currentSheepState == m_workinState;

    private void Awake()
    {
        m_stateMachine = new SheepStateMachine();

        m_idleState = new SheepIdleState(this,m_stateMachine);
        m_workinState = new SheepWorkingState(this, m_stateMachine);
        m_waitForWorkState = new SheepWaitForWorkState(this, m_stateMachine);
        m_restingState = new SheepRestingState(this, m_stateMachine);
        m_eatingState = new SheepEatingState(this, m_stateMachine);


       


    }
    private void Start()
    {

        m_animator = GetComponent<Animator>();

        //initialize state machine
        m_stateMachine.Initialize(m_idleState, m_animator);

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        
        //visual
        GenerateRandomName();
        GenerateRandomColor();

        //add sheep to the crew list
        CrewManager.Instance.AddSheep(this);
        m_assignedRoom = GetRoomInstance();
        



    }

    private void Update()
    {
        Hunger();
        Thirst();
        m_currentMorale =  (int)GetMorale();
    }

    
    private void GenerateRandomName()
    {


        int index = Random.Range(0, m_sheepNames.m_sheepNamesDataBase.Count);

        m_sheepName = m_sheepNames.m_sheepNamesDataBase[index];

        transform.gameObject.name = m_sheepName;
    }


    private void GenerateRandomColor()
    {

        m_spriteRenderer.color = Random.ColorHSV(1f,1f,0.2f,0f,1f,1f);


    }


    private void Hunger()
    {


        m_hungerElapse += Time.deltaTime;

        m_hunger = Mathf.Clamp(m_hunger, 0, 100);
        if(m_hungerElapse >= m_hungerTimer)
        {
            m_hunger--;
            m_hungerElapse = 0;
            m_hunger = Mathf.Clamp(m_hunger, 0, 100);
        }


    }

    private void Thirst()
    {
        m_thirstElapse += Time.deltaTime;

        if (m_thirstElapse >= m_thirstTimer)
        {
            m_thirst--;
            m_thirstElapse = 0;
            m_thirst = Mathf.Clamp(m_thirst, 0, 100);

        }




    }
    public void ConsumeFood(int ressource)
    {

        m_hunger += ressource;


    }


 
    public void ConsumeWater(int ressource)
    {
        m_thirst += ressource;
    }

    private void OnDestroy()
    {
        CrewManager.Instance.RemoveSheep(this);
    }

    public int GetHunger()
    {
        return m_hunger;
    }
    public int GetThirst()
    {
        return m_thirst;
    }


    //Return the average of hunger and thirst 
    //To do
    //make a better system with a paycheck
    public float GetMorale()
    {

        return (m_hunger + m_thirst) / 2;
    }



    public void SetSheepAssignedRoom(RoomInstance newAssignedRoom)
    {

        m_assignedRoom = newAssignedRoom;
    }



    private RoomInstance GetRoomInstance()
    {

        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 2.0f);
        if(hitCollider.Length > 0)
        {
            for(int i =0;i< hitCollider.Length; i++)
            {
                if (hitCollider[i].gameObject.layer == LayerMask.NameToLayer("Room"))
                {
                    return hitCollider[i].GetComponent<RoomInstance>();
                }
            }
            
        }
        return null;


    }

    public void RequestWork()
    {

        if(m_hunger < 20)
        {
            m_stateMachine.ChangeState(m_eatingState, m_animator);
            return;
        }
        m_stateMachine.ChangeState(m_workinState, m_animator);
    }

    public void SetIdle()
    {
        m_stateMachine.ChangeState(m_idleState, m_animator);
    }







}

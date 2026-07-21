
using System;
using System.Collections.Generic;
using UnityEngine;


public enum ESheepTrait
{
    None,
    HardWorker,
    Lazy,
    Glutton,
    Frugal
    

}

public enum ESheepSpeciality
{
    None,
    Farmer,
    Engineer,
    BookWorm,
    SeaWolf,
    
}


public enum ESheepJob
{
    Farmer,
    Engineer,    
    Sailor,
    Scientist
}
public class SheepInstance : MonoBehaviour
{





    [SerializeField] private SheepNamesDataBase m_sheepNames;
    [SerializeField] public string m_sheepName;
    [SerializeField] private ESheepTrait m_trait;
    [SerializeField] private ESheepSpeciality m_speciality;
    [SerializeField] private float m_hungerTimer = 5f;
    [SerializeField] private float m_thirstTimer = 2f;
    [SerializeField] private float m_dyingHealthTime = 2f;

    [SerializeField] private GameObject m_hungryToken;
    [SerializeField] private GameObject m_thirstyToken;
    [SerializeField] private GameObject m_DyingToken;

    private bool m_initializedFromRecruit = false;
    private Animator m_animator;
    private Dictionary<ESheepJob, int> m_jobXP = new();
    public ESheepTrait Trait => m_trait;
    public ESheepSpeciality Speciality => m_speciality;
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
    private float m_dyingElapse = 0;

    public bool IsWorking => m_stateMachine != null && m_stateMachine.m_currentSheepState == m_workinState;

    private void Awake()
    {
        m_stateMachine = new SheepStateMachine();

        m_idleState = new SheepIdleState(this, m_stateMachine);
        m_workinState = new SheepWorkingState(this, m_stateMachine);
        m_waitForWorkState = new SheepWaitForWorkState(this, m_stateMachine);
        m_restingState = new SheepRestingState(this, m_stateMachine);
        m_eatingState = new SheepEatingState(this, m_stateMachine);

        //initialize xp dictionary 
        foreach (ESheepJob job in Enum.GetValues(typeof(ESheepJob)))
        {
            m_jobXP[job] = 0;



        }




    }
    private void Start()
    {

        m_animator = GetComponent<Animator>();
        m_hungryToken.SetActive(false);
        m_thirstyToken.SetActive(false);
        m_DyingToken.SetActive(false);
        //initialize state machine
        m_stateMachine.Initialize(m_idleState, m_animator);

        m_spriteRenderer = GetComponent<SpriteRenderer>();

       

        if (m_initializedFromRecruit == false)
        {
            GenerateRandomName();
            AssignedRandomTraitAndSpeciality();
           
        }




        //add sheep to the crew list
        CrewManager.Instance.AddSheep(this);
        m_assignedRoom = GetRoomInstance();




    }

    private void Update()
    {
        Hunger();
        Thirst();
        Dying();
        m_currentMorale = (int)GetMorale();
    }


    private void GenerateRandomName()
    {


        int index = UnityEngine.Random.Range(0, m_sheepNames.m_sheepNamesDataBase.Count);

        m_sheepName = m_sheepNames.m_sheepNamesDataBase[index];

        transform.gameObject.name = m_sheepName;
    }


    private void GenerateColorByTrait()
    {
        Color color = Color.white;


        switch (Trait)
        {
            case ESheepTrait.None:
                {

                    color = Color.white;
                    break;
                }
            case ESheepTrait.HardWorker:
                {
                    color = new Color32(188, 56, 66,255);

                    break;
                }

            case ESheepTrait.Lazy:
                {
                    color = new Color32(252, 15, 66, 255);

                    break;
                }
            case ESheepTrait.Glutton:

                {
                    color = new Color32(30, 52, 66, 255);

                    break;
                }

        }



        m_spriteRenderer.color = color;


    }


    private void Hunger()
    {


        m_hungerElapse += Time.deltaTime;

        m_hunger = Mathf.Clamp(m_hunger, 0, 100);
        if (m_hungerElapse >= m_hungerTimer)
        {
            m_hunger--;
            m_hungerElapse = 0;
            m_hunger = Mathf.Clamp(m_hunger, 0, 100);
        }

        if(m_hunger <= 30)
        {
            m_hungryToken.SetActive(true);
        }
        else
        {
            m_thirstyToken.SetActive(false);
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

        if(m_thirst <= 30)
        {
            m_thirstyToken.SetActive(true);
        }
        else
        {
            m_thirstyToken.SetActive(false);
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
        if (hitCollider.Length > 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
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

        if (m_hunger < 20)
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

    public void AddJobXp(ESheepJob job,int amount)
    {
        m_jobXP[job] += amount;
    }

    private void AssignedRandomTraitAndSpeciality()
    {
        Array traits = Enum.GetValues(typeof(ESheepTrait));
        Array speciality = Enum.GetValues(typeof (ESheepSpeciality));
        m_trait = (ESheepTrait)traits.GetValue(UnityEngine.Random.Range(1, traits.Length));
        m_speciality = (ESheepSpeciality)speciality.GetValue(UnityEngine.Random.Range(1, speciality.Length));
    }

    
    public int GetJobLevel(ESheepJob job)
    {
        int xp = m_jobXP[job];


        if (xp >= 500) return 5;
        if (xp >= 250) return 4;
        if (xp >= 100) return 3;
        if (xp >= 25) return 2;

        return 1;
    }

    public void InitializeFromRecruitData(SheepRecruitData recruitData)
    {
        m_initializedFromRecruit = true;

        m_sheepName = recruitData.name;
        gameObject.name = m_sheepName;

        m_trait = recruitData.trait;
        m_speciality = recruitData.speciality;

        m_jobXP[ESheepJob.Farmer] = recruitData.farmerXp;
        m_jobXP[ESheepJob.Engineer] = recruitData.engineerXp;
        m_jobXP[ESheepJob.Sailor] = recruitData.sailorXp;
    }


    public void Dying()
    {
        if(m_hunger <= 0 && m_thirst <= 0)
        {
            m_hungryToken.SetActive(false);
            m_thirstyToken.SetActive(false);
            m_DyingToken.SetActive(true);
           
            m_dyingElapse += Time.deltaTime;

            if(m_dyingElapse >= m_dyingHealthTime)
            {
                m_currentHp--;
                m_dyingElapse = 0;
            }
        }

        if(m_currentHp <= 0)
        {
            UiPanelManager.Instance.closeSheepPanel();
            Destroy(this.gameObject);
        }
    }
}

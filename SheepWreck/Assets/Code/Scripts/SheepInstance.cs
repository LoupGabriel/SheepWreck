using UnityEngine;

public class SheepInstance : MonoBehaviour
{



    [SerializeField] private SheepNamesDataBase m_sheepNames;
    [SerializeField] public string m_sheepName;

    [SerializeField] private float m_hungerTimer = 5f;
    [SerializeField] private float m_thirstTimer = 2f;

    public int m_currentHp;
    public int m_MaxHp;

    public int level;

    

    //ressource
    public int m_currentMorale;

    public int m_hunger;
    public int m_thirst;

    public RoomInstance assignedRoom;
    private float m_hungerElapse = 0;
    private float m_thirstElapse = 0;
    private void Awake()
    {
        GenerateRandomName();
        CrewManager.Instance.AddSheep(this);

    }

    private void Update()
    {
        Hunger();
        Thirst();
    }


    private void GenerateRandomName()
    {


        int index = Random.Range(0, m_sheepNames.m_sheepNamesDataBase.Count);

        m_sheepName = m_sheepNames.m_sheepNamesDataBase[index];

        transform.gameObject.name = m_sheepName;
    }

    private void Hunger()
    {


        m_hungerElapse += Time.deltaTime;

        if(m_hungerElapse >= m_hungerTimer)
        {
            m_hunger--;
            m_hungerElapse = 0;
           
        }


    }

    private void Thirst()
    {
        m_thirstElapse += Time.deltaTime;

        if (m_thirstElapse >= m_thirstTimer)
        {
            m_thirst--;
            m_thirstElapse = 0;

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


}

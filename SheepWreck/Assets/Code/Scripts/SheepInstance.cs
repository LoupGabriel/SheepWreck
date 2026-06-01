using UnityEngine;

public class SheepInstance : MonoBehaviour
{

    private enum SheepState
    {
        idle,
        getRessource,
        produce,
        die
    }

    [SerializeField] private SheepNamesDataBase m_sheepNames;
    [SerializeField] public string m_sheepName;

    [SerializeField] private float m_hungerTimer = 5f;
    [SerializeField] private float m_thirstTimer = 2f;


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

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        GenerateRandomName();
        GenerateRandomColor();
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

    private void GenerateRandomColor()
    {

        m_spriteRenderer.color = Random.ColorHSV(1f,1f,0f,0.5f,0.5f,1f);


    }
    private void Hunger()
    {


        m_hungerElapse += Time.deltaTime;

        m_hunger = Mathf.Clamp(m_hunger, 0, 100);
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


    private void Produced()
    {




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
    //To do make a better system with a paycheck
    public float GetMorale()
    {

        

        return (m_hunger + m_thirst) / 2;
    }
}

using UnityEngine;

public class SheepInstance : MonoBehaviour
{



    [SerializeField] private SheepNamesDataBase m_sheepNames;
    [SerializeField] public string m_sheepName;

    public int m_currentHp;
    public int m_MaxHp;

    public int level;

    

    //ressource
    public int m_currentMorale;

    public int m_hunger;
    public int m_thirst;

    public RoomInstance assignedRoom;

    private void Awake()
    {
        GenerateRandomName();
    }

    private void GenerateRandomName()
    {


        int index = Random.Range(0, m_sheepNames.m_sheepNamesDataBase.Count);

        m_sheepName = m_sheepNames.m_sheepNamesDataBase[index];

        transform.gameObject.name = m_sheepName;
    }



}

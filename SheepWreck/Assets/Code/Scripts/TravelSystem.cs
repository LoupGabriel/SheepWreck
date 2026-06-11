using System;
using UnityEngine;

public class TravelSystem : MonoBehaviour
{
    public static TravelSystem Instance { get; private set; }

    public Action<Vector3> OnDestinationReach;

    public IslandInstance m_currentIsland;

    public IslandInstance m_destinationIsland;

    public GameObject m_islandPanel;

    [SerializeField] private int m_energyCostPerTile = 25;
    public bool m_isTraveling = false;

    public float m_distanceToTravel;
    public float m_travelTime =0;


    public float m_timePerTile = 5f;

    public bool m_canTravel = false;


    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (!m_isTraveling)
        {
            return;
        }

        if (m_canTravel)
        {
            m_travelTime -= Time.deltaTime;
        }

      



        if (m_travelTime <= 0f)
        {
            ArriveAtDestination();
        }
    }

    public void SetDestination(IslandInstance targetIstland)
    {
        if (m_isTraveling || targetIstland == null) return;


        int distance = GetManhattanDistance(m_currentIsland.m_gridPos, targetIstland.m_gridPos);

        int energyCost = distance * m_energyCostPerTile;

        int currentEnergy = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY];



        if (currentEnergy < energyCost)
        {
            Debug.Log("Not enough energy!");
            m_canTravel = false;

            return;

        }
        m_canTravel = true;
        RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY] -= energyCost;
        m_destinationIsland = targetIstland;
        m_travelTime = distance * m_timePerTile;
        m_isTraveling = true;

    }




    private int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }


    private void ArriveAtDestination()
    {
        Debug.Log("Arrive at destination");
        m_currentIsland = m_destinationIsland;
        OnDestinationReach?.Invoke(m_currentIsland.transform.position);
        m_travelTime = 0f;
        m_destinationIsland = null;
        m_isTraveling = false;


    }





}

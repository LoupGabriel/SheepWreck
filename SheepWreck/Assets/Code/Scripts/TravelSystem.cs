using System;
using UnityEngine;

public class TravelSystem : MonoBehaviour
{
    public static TravelSystem Instance { get; private set; }

    public Action<Vector3> OnDestinationReach;
    public Action OnDestinationSet;

    public IslandInstance m_currentIsland;

    public IslandInstance m_destinationIsland;

    public GameObject m_islandPanel;

    private int m_currentEnvironment = 0;
   
    [SerializeField] private Animator[] m_sailAnimator;

    [SerializeField] private int m_energyCostPerTile = 25;
    [SerializeField] private EnvironmentController m_environment;
    public bool m_isTraveling = false;

    public float m_distanceToTravel;
    public float m_travelTime =0;


    public float m_timePerTile = 5f;

    public bool m_canTravel = false;
    private float m_lastEventThreshold = 1f;

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
            if(PauseController.m_isPaused) { return; }
            
            m_travelTime -= Time.deltaTime;

            float progress = 1f - (m_travelTime / m_distanceToTravel);
            TryTravelEvent(progress);
           


        }





        if (m_travelTime <= 0f)
        {
            ArriveAtDestination();
            
        }
    }

    /// <summary>
    /// Try to trigger an event at each quarter of the travel
    /// </summary>
    /// <param name="progress"></param>
    private void TryTravelEvent(float progress)
    {
        float[] thresholds = { 0.25f, 0.5f, 0.75f };

        foreach (float t in thresholds)
        {
            if (progress >= t && m_lastEventThreshold < t)
            {
                EventManager.Instance.TryTriggerEvent();
                m_lastEventThreshold = t;
                break;
            }
        }
    }
    public void SetDestination(IslandInstance targetIstland)
    {
        if (m_isTraveling || targetIstland == null) return;

      
        int distance = GetManhattanDistance(m_currentIsland.m_gridPos, targetIstland.m_gridPos);

        int energyCost = distance * m_energyCostPerTile;

        int currentEnergy = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY];


        m_lastEventThreshold = 0f;

        if (currentEnergy < energyCost)
        {
            
            SfxManager.PlaySfx("Error");
            m_canTravel = false;

            return;

        }
        m_canTravel = true;
        foreach (var animator in m_sailAnimator)
        {
            animator.SetTrigger("Sailing");
        }
        RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY] -= energyCost;
        RessourceSystem.Instance.OnRessourceChange?.Invoke(ERessourceType.ENERGY);

        m_destinationIsland = targetIstland;

        m_travelTime = distance * m_timePerTile;

        m_distanceToTravel = m_travelTime;

        m_isTraveling = true;
        EventManager.Instance.SetContext(true);
        OnDestinationSet?.Invoke();
        SoundtrackManager.Instance.PlayMusic("SetSail");
        m_environment.SetSailSpeed(5f);



    }




    private int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }


    private void ArriveAtDestination()
    {
        foreach (Animator animator in m_sailAnimator)
        {
            animator.SetTrigger("Idle");
        }
        m_currentEnvironment++;
        if (m_currentEnvironment >= Enum.GetValues(typeof(EEnvironment)).Length)
        {
            m_currentEnvironment = 0;
        }
        m_environment.ChangeBackground(m_currentEnvironment);

        m_currentIsland = m_destinationIsland;
        OnDestinationReach?.Invoke(m_currentIsland.transform.position);
        m_travelTime = 0f;
        m_destinationIsland = null;
        m_isTraveling = false;
        EventManager.Instance.SetContext(false);
        SoundtrackManager.Instance.PlayMusic("MainMusic");
        m_environment.SetSailSpeed(1f);

       
    }





}

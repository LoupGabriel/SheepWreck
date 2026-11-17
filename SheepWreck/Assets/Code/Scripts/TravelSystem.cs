using System;
using UnityEngine;

public class TravelSystem : Singleton<TravelSystem>
{
    public Action<Vector3> OnDestinationReach;
    public Action OnDestinationSet;
    //Tutorial event
    public static event System.Action OnFirstSail;

    [Header("Game Settings")]
    [SerializeField] private IslandInstance m_startingIsland;
    [SerializeField] private int m_energyCostPerTile = 25;

    [Header("Visual")]
    [SerializeField] private Animator[] m_sailAnimator;
    [SerializeField] private EnvironmentController m_environment;
    [SerializeField] private Animator m_anchorAnimator;
    [SerializeField] private Transform m_islandVisualParent;

    private IslandInstance m_currentIsland;
    private IslandInstance m_destinationIsland;
    private GameObject m_spawnedIsland;
    private int m_currentEnvironment = 0;
    private bool m_isTraveling = false;
    private float m_distanceToTravel;
    private float m_travelTime =0;
    private float m_timePerTile = 5f;
    private bool m_canTravel = false;
    private float m_lastEventThreshold = 1f;

    //Getter
    public bool IsTraveling => m_isTraveling;
    public float TravelTime => m_travelTime;
    public IslandInstance CurrentIsland => m_currentIsland;
    

    private void Awake()
    {
        base.Awake();
        m_currentIsland = m_startingIsland;
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

        if (m_travelTime <= 0f && m_isTraveling)
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
        float[] thresholds = {  0.5f};

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
    public void SetDestination(IslandInstance targetIsland)
    {
        if (m_isTraveling || targetIsland == null) return;

      
        int distance = GetManhattanDistance(m_currentIsland.GridPos, targetIsland.GridPos);

        int energyCost = GetEnergyCost(targetIsland);

        int currentEnergy = RessourceSystem.Instance.RessourceDictionary[ERessourceType.ENERGY];


        m_lastEventThreshold = 0f;

        if (currentEnergy < energyCost)
        {
            
            SfxManager.PlaySfx("Error");
            UiNotification.instance.TriggerNotification("not enough energy");
            m_canTravel = false;

            return;

        }
        m_canTravel = true;
        foreach (var animator in m_sailAnimator)
        {
            animator.SetTrigger("Sailing");
        }
       
        RessourceSystem.Instance.RessourceDictionary[ERessourceType.ENERGY] -= energyCost;
        RessourceSystem.Instance.OnRessourceChange?.Invoke(ERessourceType.ENERGY);

        m_destinationIsland = targetIsland;
      
        m_travelTime = distance * m_timePerTile;

        m_distanceToTravel = m_travelTime;

        m_isTraveling = true;
        EventManager.Instance.SetContext(true);
        OnDestinationSet?.Invoke();
        UiPanelManager.Instance.ShowIslandViewButton(false);
       
        SoundtrackManager.Instance.PlayMusic("SetSail");
        m_environment.SetSailSpeed(10f);
        DestroyIsland();
    }
    private int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    /// <summary>
    /// call when the destination is reach
    /// </summary>
    private void ArriveAtDestination()
    {
        OnFirstSail?.Invoke();
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
        SpawnIslandVisual();
        m_travelTime = 0f;
        m_destinationIsland = null;
        m_isTraveling = false;
        EventManager.Instance.SetContext(false);
        SoundtrackManager.Instance.PlayMusic("MainMusic");
        m_environment.SetSailSpeed(1f);
        m_environment.StopSailEffects();
        UiNotification.instance.TriggerNotification($"Destination Reach{m_currentIsland.IslandName}");

        //Anchor
        SfxManager.PlaySfx("anchorDrop");
        DownAnchor();
        UiPanelManager.Instance.ShowIslandViewButton(true);

        GameManager.Instance.ChangeState(new GameManagementState(GameManager.Instance));
    }

    /// <summary>
    /// Return the total energy cost of the current selected travel
    /// </summary>
    /// <param name="targetIsland"></param>
    /// <returns></returns>
    public int GetEnergyCost(IslandInstance targetIsland)
    {
        if(targetIsland == null ||m_currentIsland == null) return 0;

        int distance = GetManhattanDistance(m_currentIsland.GridPos, targetIsland.GridPos);

        return distance * m_energyCostPerTile;
    }

    private void SpawnIslandVisual()
    {
        GameObject visual = m_destinationIsland.IslandVisual;
        m_spawnedIsland = Instantiate(visual, m_islandVisualParent);
    }

    public void DestroyIsland()
    {
        Destroy(m_spawnedIsland);
        m_spawnedIsland = null;
    }

    public void UpAnchor()
    {
        m_anchorAnimator.SetTrigger("AnchorUp");
    }
    public void DownAnchor()
    {
        m_anchorAnimator.SetTrigger("AnchorDrop");
    }


}

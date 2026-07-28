
using System.Collections.Generic;
using UnityEngine;

public class ResearchSystem : MonoBehaviour
{
    public static ResearchSystem Instance;


    [SerializeField]
    private List<ResearchProgress> m_researchProgress = new();


    [SerializeField] private List<RoomData> m_upgradableCapacity;
    [SerializeField] private List<RoomData> m_upgradableSheepSlot;
    private ResearchProgress m_currentResearch;


    [SerializeField] private Transform m_roomPanelContainer;

    private int m_currentShipUpgrade = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (m_currentResearch == null)
            return;

        m_currentResearch.currentProgress += Time.deltaTime;

        if (m_currentResearch.currentProgress >= m_currentResearch.researchData.researchTime)
        {
            CompleteResearch(m_currentResearch);
        }
    }


    /// <summary>
    /// Start the selected research if condition are check
    /// </summary>
    /// <param name="research"></param>
    public void StartResearch(ResearchNodeSo research)
    {
        if (m_currentResearch != null)
            return;

        if (!CanResearch(research))
            return;
        int researchPoint = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.RESEARCH];
        if (researchPoint < research.researchCost)
        {
            UiNotification.instance.TriggerNotification("Not enough Research points");
            return;
        }
        else
        {
            RessourceSystem.Instance.GainRessource(-research.researchCost, ERessourceType.RESEARCH);

        }

        ResearchProgress progress = FindProgress(research);

        if (progress == null || progress.isCompleted)
            return;

        m_currentResearch = progress;
        SfxManager.PlaySfx("Research");
        UiNotification.instance.TriggerNotification($"Curently researching {m_currentResearch.researchData.name}");
    }

    /// <summary>
    /// Complete the active research
    /// </summary>
    /// <param name="progress"></param>
    private void CompleteResearch(ResearchProgress progress)
    {
        progress.isCompleted = true;
        progress.isUnlocked = true;
        progress.currentProgress = progress.researchData.researchTime;

        ApplyReward(progress.researchData);
        m_currentResearch = null;
        UiNotification.instance.TriggerNotification($"Completed: {progress.researchData.name}");
    }



    /// <summary>
    /// Apply the research reward
    /// </summary>
    /// <param name="research"></param>
    private void ApplyReward(ResearchNodeSo research)
    {
        switch (research.rewardType)
        {
            case EResearchRewardType.INCREASE_STORAGE:

                UpgradeCapacity((int)research.rewardValue,research.m_ressourceType);



                break;

            case EResearchRewardType.INCREASE_CREW_CAPACITY:

                UpgradeSheepCapacity((int)research.rewardValue, research.m_ressourceType);
                break;
            case EResearchRewardType.REDUCE_TRAVEL_COST:

                break;

            case EResearchRewardType.UNLOCK_ISLAND:

                break;

            case EResearchRewardType.UPGRADE_SHIP:
                m_currentShipUpgrade++;
                if (m_currentShipUpgrade == 1)
                {
                    ShipSystem.Instance.UpgradeShip();
                    SfxManager.PlaySfx("Upgrade01");
                }
                else if (m_currentShipUpgrade == 2)
                {
                    ShipSystem.Instance.Upgrade02Ship();
                    SfxManager.PlaySfx("Upgrade02");
                }
                break;

            case EResearchRewardType.UNLOCK_ROOM:
                GameObject room = research.roomPanelToUnlock;
                SfxManager.PlaySfx("UpgradeGeneric");

                Instantiate(room, m_roomPanelContainer);

                break;
        }
    }

    /// <summary>
    /// return true if all condition are met
    /// </summary>
    /// <param name="research"></param>
    /// <returns></returns>
    public bool CanResearch(ResearchNodeSo research)
    {
        foreach (ResearchNodeSo prerequisite in research.prerequissites)
        {
            if (!IsResearchComplete(prerequisite))
            {
                return false;
            }
        }

        return true;

    }

    public bool IsResearchUnlock(ResearchNodeSo research)
    {
        ResearchProgress progress = FindProgress(research);
        if (research.prerequissites.Count == 0)
            return true;
        foreach (ResearchNodeSo prerequisite in research.prerequissites)
        {
            if (!IsResearchComplete(prerequisite))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsResearchComplete(ResearchNodeSo research)
    {
        ResearchProgress progress = FindProgress(research);
        return progress != null && progress.isCompleted;
    }
    public ResearchProgress FindProgress(ResearchNodeSo research)
    {
        return m_researchProgress.Find(x => x.researchData == research);
    }



    private void UpgradeCapacity(int amount,ERessourceType type)
    {
        foreach(RoomData room in m_upgradableCapacity)
        {
            if(room.m_ressourceProduced == type)
            {
                room.m_capacity += amount;
            }
            
               
            
            
        }
    }
    private void UpgradeSheepCapacity(int amount,ERessourceType type)
    {
        foreach(RoomData room in m_upgradableSheepSlot)
        {
            if(room.m_ressourceProduced == type)
            {
                room.m_maxSheepCapacity += amount;
            }
            
               
            
            
        }
    }
}

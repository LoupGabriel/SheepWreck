
using System.Collections.Generic;
using UnityEngine;

public class ResearchSystem : MonoBehaviour
{
    public static ResearchSystem Instance;


    [SerializeField]
    private List<ResearchProgress> m_researchProgress = new();

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

    public void StartResearch(ResearchNodeSo research)
    {
        if (m_currentResearch != null)
            return;

        if (!CanResearch(research))
            return;
        int researchPoint = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.RESEARCH];
        if (researchPoint < research.researchCost)
        {
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
    }
    private void CompleteResearch(ResearchProgress progress)
    {
        progress.isCompleted = true;
        progress.isUnlocked = true;
        progress.currentProgress = progress.researchData.researchTime;

        ApplyReward(progress.researchData);
        m_currentResearch = null;
    }
    private void ApplyReward(ResearchNodeSo research)
    {
        switch (research.rewardType)
        {
            case EResearchRewardType.INCREASE_STORAGE:

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
                }
                else if (m_currentShipUpgrade == 2)
                {
                    ShipSystem.Instance.Upgrade02Ship();
                }
                break;

            case EResearchRewardType.UNLOCK_ROOM:
                GameObject room = research.roomPanelToUnlock;

                Instantiate(room, m_roomPanelContainer);

                break;
        }
    }

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
}

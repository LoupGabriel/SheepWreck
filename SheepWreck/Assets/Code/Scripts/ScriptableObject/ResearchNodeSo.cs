using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Research/Research Node")]
public class ResearchNodeSo : ScriptableObject
{
    [Header("Information")]
    public string researchName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Header("Progression")]
    public int researchCost;
    public float researchTime;

    public List<ResearchNodeSo> prerequissites;

    [Header("Reward")]
    public EResearchRewardType rewardType;
    public float rewardValue;

    public RoomData roomToUnlock;
}

public enum EResearchRewardType
{
    UNLOCK_ROOM,
    INCREASE_STORAGE,
    INCREASE_PRODUCTION,
    REDUCE_TRAVEL_COST,
    REDUCE_TRAVEL_TIME,
    INCREASE_CREW_CAPACITY,
    UNLOCK_ISLAND
}

[System.Serializable]
public class ResearchProgress
{
    public ResearchNodeSo researchData;
    public bool isUnlocked;
    public bool isCompleted;
    public float currentProgress;
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.VisualScripting;


[CreateAssetMenu(menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{

    public string m_questID;
    public string m_questName;
    
    public string m_description;

    public int m_recompense;
   


    
   public List<QuestObjectives> m_objectives;


    private void OnValidate()
    {
        if (string.IsNullOrEmpty(m_questID))
        {
            m_questID = m_questName + Guid.NewGuid().ToString();
        }
    }

}




[System.Serializable]

public class QuestObjectives
{
    public string m_objectiveID;

    public string m_description;

    public EObjectiveType m_type;

    public ERessourceType m_ressource;

    public int m_requiredAmount;
    public int m_currentAmount;

    public Vector2Int m_questDestination;

    public bool IsCompleted => m_currentAmount >= m_requiredAmount;
}


public enum EObjectiveType
{
    DeliverItem,
    ReachLocation,
}


[System.Serializable]
public class QuestProgress
{
    public Quest m_quest;
    public List<QuestObjectives> m_objectives;
    public bool m_rewardGiven = false;

    public QuestProgress(Quest quest)
    {
        this.m_quest = quest;
        m_objectives = new List<QuestObjectives>();


        //avoid modifying original quest

        foreach (var obj in quest.m_objectives)
        {
            m_objectives.Add(new QuestObjectives
            {
                m_objectiveID = obj.m_objectiveID,
                m_description = obj.m_description,
                m_type = obj.m_type,
                m_ressource = obj.m_ressource,
                m_requiredAmount = obj.m_requiredAmount,
                m_currentAmount = 0,
                m_questDestination = obj.m_questDestination,


            });


        }
    }


    public bool IsCompleted => m_objectives.TrueForAll(o => o.IsCompleted);
    public string QuestID => m_quest.m_questID;
}

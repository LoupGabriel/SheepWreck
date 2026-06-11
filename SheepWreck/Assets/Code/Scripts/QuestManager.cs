
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<QuestProgress> m_activesQuests = new();

    private UIQuest m_questUi;

    public  Action<Quest> OnQuestAccepted;
    public  Action<ERessourceType, int, Vector2Int> OnQuestDelivered;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        m_questUi = FindAnyObjectByType<UIQuest>();
    }

    private void OnEnable()
    {
        OnQuestAccepted += AcceptQuest;
        OnQuestDelivered += HandleDelivery;
        
      


    }

   

    private void OnDisable()
    {
        OnQuestAccepted -= AcceptQuest;
        OnQuestDelivered -= HandleDelivery;
      
    }

    public void AcceptQuest(Quest quest)
    {
        //only one instance off a quest
       if (IsQuestActive(quest.m_questID)) return;

        m_activesQuests.Add(new QuestProgress(quest));
        m_questUi.UpdateQuestUi();


    }


    

    public bool IsQuestActive(string questID) => m_activesQuests.Exists(q => q.QuestID == questID);

    public bool IsQuestCompleted(string questID)
    {
        QuestProgress quest = m_activesQuests.Find(q => q.QuestID == questID);

        return quest != null && quest.m_objectives.TrueForAll(o => o.IsCompleted);
    }


    public void HandleDelivery(ERessourceType type,int amount , Vector2Int isLandPos)
    {
        foreach(var quest in m_activesQuests)
        {
            foreach(var obj in quest.m_objectives)
            {
                if (obj.m_type != EObjectiveType.DeliverItem) continue;
                if (obj.m_ressource != type) continue;
                if (obj.m_questDestination != isLandPos) continue;

                obj.m_currentAmount += amount;
            }
        }

        CheckQuestCompletion();
    }

   

    private void CheckQuestCompletion()
    {
        foreach(var quest in m_activesQuests)
        {
            if (quest.IsCompleted && !quest.m_rewardGiven)
            {
                Debug.Log("Quest Complete" + quest.m_quest.m_questName);

                quest.m_rewardGiven = true;
                RessourceSystem.Instance.GainRessource(quest.m_quest.m_recompense, ERessourceType.GOLD);
            }
        }

        m_questUi.UpdateQuestUi();
    }
}




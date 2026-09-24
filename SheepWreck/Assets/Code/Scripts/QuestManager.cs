
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


    /// <summary>
    /// Add quest to active quest list.Cant accepted a same quest twice.
    /// </summary>
    /// <param name="quest"></param>
    public void AcceptQuest(Quest quest)
    {
        //only one instance off a quest
       if (IsQuestActive(quest.m_questID)) return;

        m_activesQuests.Add(new QuestProgress(quest));
        m_questUi.UpdateQuestUi();


    }


    
    //look in the list for an active quest with the same id
    public bool IsQuestActive(string questID) => m_activesQuests.Exists(q => q.QuestID == questID);

    public bool IsQuestCompleted(string questID)
    {
        //look for the activequest and return the one with the same id
        QuestProgress quest = m_activesQuests.Find(q => q.QuestID == questID);


        //return true if all objective are complete
        return quest != null && quest.m_objectives.TrueForAll(o => o.IsCompleted);
    }

    /// <summary>
    /// Adding ressource to the current quest progression
    /// </summary>
    /// <param name="type">type of ressource</param>
    /// <param name="amount">amount deliver</param>
    /// <param name="isLandPos">is at the same island as the quest</param>
    public void HandleDelivery(ERessourceType type, int amount, Vector2Int isLandPos)
    {
        

        foreach (var quest in m_activesQuests)
        {
            foreach (var obj in quest.m_objectives)
            {
                
                if (obj.m_type != EObjectiveType.DeliverItem) continue;
                if (obj.m_ressource != type) continue;         
                if (obj.m_questDestination != isLandPos) continue;
               
                obj.m_currentAmount += amount;

              
            }
        }

        CheckQuestCompletion();
    }


    /// <summary>
    /// Complete a quest and get the reward.Cant get the reward twice
    /// </summary>
    private void CheckQuestCompletion()
    {
        foreach(var quest in m_activesQuests)
        {
            if (quest.IsCompleted && !quest.m_rewardGiven)
            {
                SfxManager.PlaySfx("Complete");
                SfxManager.PlaySfx("Coin");

                quest.m_rewardGiven = true;
                RessourceSystem.Instance.GainRessource(quest.m_quest.m_recompense, ERessourceType.GOLD);
            }
        }

        m_questUi.UpdateQuestUi();
    }
}




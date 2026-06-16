
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuest : MonoBehaviour
{
    [SerializeField] private Transform m_questListContent;

    [SerializeField] private GameObject m_questEntryPrefab;

    [SerializeField] private GameObject m_objectiveTextPrefab;


    private void Start()
    {
        
        UpdateQuestUi();

    }

    public void UpdateQuestUi()
    {
        //destroy all game object to refresh it
        foreach (Transform child in m_questListContent)
        {

            Destroy(child.gameObject);

        }


        foreach (var quest in QuestManager.Instance.m_activesQuests)
        {
            GameObject entry = Instantiate(m_questEntryPrefab, m_questListContent);
            TMP_Text questNameText = entry.transform.Find("QuestNameText").GetComponent<TMP_Text>();
            Transform objectiveList = entry.transform.Find("ObjectivesList");



            questNameText.text = quest.m_quest.m_questName;

            foreach(var objective in quest.m_objectives)
            {
                GameObject objectText = Instantiate(m_objectiveTextPrefab, objectiveList);
                
                 TMP_Text objText = objectText.transform.Find("ObjectiveText").GetComponent<TMP_Text>();

                TMP_Text destinationText = objectText.transform.Find("DestinationText").GetComponent<TMP_Text>();

                objText.text = $"{objective.m_description}({objective.m_currentAmount} / {objective.m_requiredAmount})";
                destinationText.text = $"Destinatin : {objective.m_questDestination}";
            }

        }
    }
}

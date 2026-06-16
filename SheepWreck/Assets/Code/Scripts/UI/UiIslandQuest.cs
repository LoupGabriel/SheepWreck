
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiIslandQuest : MonoBehaviour
{
    private IslandInstance m_currentIsland;

    [SerializeField] private Transform m_questContainer;
    [SerializeField] private GameObject m_questPrefab;
    [SerializeField] private GameObject m_questPanel;

    [SerializeField] private Button m_DeliveryButton;
    [SerializeField] private TMP_Dropdown m_ressourceDropdown;
    [SerializeField] private TMP_Dropdown m_amountDropdown;
    private void Start()
    {

        m_DeliveryButton.onClick.AddListener(OnDeliverClicked);
        m_questPanel.SetActive(false);



        //delivery

        m_ressourceDropdown.ClearOptions();
        List<string> options = new List<string>();

        //add enum to dropdown
        foreach(ERessourceType type in Enum.GetValues(typeof(ERessourceType)))
        {

            options.Add(type.ToString());

        }
        m_ressourceDropdown.AddOptions(options);
        m_ressourceDropdown.onValueChanged.AddListener(OnResourceChanged);


    }

    private void OnResourceChanged(int index)
    {
        SetupAmountDropdown();
    }

    private void SetupAmountDropdown()
    {
        m_amountDropdown.ClearOptions();

        int available = GetSelectedResourceAmount();

        List<string> options = new List<string>
    {
        "10",
        "25",
        "50",
        "100",
        "MAX"
    };

        m_amountDropdown.AddOptions(options);
    }


    private ERessourceType GetSelectedResource()
    {
        string value = m_ressourceDropdown.options[m_ressourceDropdown.value].text;
        return (ERessourceType)Enum.Parse(typeof(ERessourceType), value);
    }

    private int GetSelectedResourceAmount()
    {
        ERessourceType type = GetSelectedResource();

        return RessourceSystem.Instance.m_ressourceDictionary[type];
    }

    private void OnDestroy()
    {
        m_DeliveryButton.onClick.RemoveListener(OnDeliverClicked);
    }

    private void OnDeliverClicked()
    {
        ERessourceType type = GetSelectedResource();

        int available = RessourceSystem.Instance.m_ressourceDictionary[type];

        int amount = GetSelectedAmount(available);
        if (amount <= 0) return;

        RessourceSystem.Instance.m_ressourceDictionary[type] -= amount;
        RessourceSystem.Instance.OnRessourceChange?.Invoke(type);

        QuestManager.Instance.OnQuestDelivered?.Invoke(
            type,
            amount,
            m_currentIsland.m_gridPos
        );
    }

    private int GetSelectedAmount(int available)
    {
        string selected = m_amountDropdown.options[m_amountDropdown.value].text;

        if (selected == "MAX")
            return available;

        return Mathf.Min(int.Parse(selected), available);
    }
    public void ActiveQuestPanel()
    {
        m_questPanel.SetActive(!m_questPanel.activeSelf);
        m_currentIsland = TravelSystem.Instance.m_currentIsland;
        UpdateCurrentQuest();

    }

    private void UpdateCurrentQuest()
    {
        foreach (Transform child in m_questContainer)
        {

            Destroy(child.gameObject);

        }


        foreach (var quest in m_currentIsland.m_availableQuest)
        {
            GameObject entry = Instantiate(m_questPrefab, m_questContainer);
            TMP_Text questNameText = entry.transform.Find("QuestNameText").GetComponent<TMP_Text>();


            Button acceptButton = entry.transform.Find("AcceptButton").GetComponent<Button>();

            TMP_Text quantityText = entry.transform.Find("QuantityText").GetComponent<TMP_Text>();
            TMP_Text ressourceText = entry.transform.Find("RessourceText").GetComponent<TMP_Text>();
            TMP_Text destinationText = entry.transform.Find("IslandNameText").GetComponent<TMP_Text>();



            questNameText.text = quest.m_questName;
            quantityText.text = quest.m_objectives[0].m_requiredAmount.ToString();
            ressourceText.text = quest.m_objectives[0].m_ressource.ToString();

            Vector2Int destination = quest.m_objectives[0].m_questDestination;
            destinationText.text = $"destination : {destination.x} / {destination.y}";


            acceptButton.onClick.RemoveAllListeners();

            acceptButton.onClick.AddListener(() =>
            {
                SfxManager.PlaySfx("Click");
                QuestManager.Instance.OnQuestAccepted?.Invoke(quest);
                Destroy(entry);

            });



        }
    }
}

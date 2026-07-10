using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiIslandRecruit : MonoBehaviour
{
    private IslandInstance m_currentIsland;
    [SerializeField] private Transform m_RecruitContainer;
    [SerializeField] private GameObject m_recruitIslandPanel;
    [SerializeField] private GameObject m_RecruitPanel;
   

   
    public void ActiveRecruitPanel()
    {
       // m_recruitIslandPanel.SetActive(!m_recruitIslandPanel.activeSelf);

        m_currentIsland = TravelSystem.Instance.m_currentIsland;
        UpdateCurrentRecruit();

    }


    private void UpdateCurrentRecruit()
    {
        //Destroy old recruit
        foreach(Transform child in m_RecruitContainer)
        {
            Destroy(child.gameObject);
        }

        foreach(SheepRecruitData recruit in m_currentIsland.m_availableRecruits)
        {
            GameObject entry = Instantiate(m_RecruitPanel, m_RecruitContainer);

            //get text
            UiRecruitPanel recruitPanel = entry.GetComponent<UiRecruitPanel>();

            TMP_Text recruitName = entry.transform.Find("Name").GetComponent<TMP_Text>();
            TMP_Text recruitTrait = entry.transform.Find("Trait").GetComponent<TMP_Text>();
            TMP_Text recruitSpeciality = entry.transform.Find("Speciality").GetComponent<TMP_Text>();
            TMP_Text recruitCost = entry.transform.Find("Cost").GetComponent<TMP_Text>();

            Button recruitButton = entry.GetComponentInChildren<Button>();
            recruitPanel.m_currentRecruit = recruit;
            recruitName.text = recruit.name;
            recruitTrait.text = recruit.trait.ToString();
            recruitSpeciality.text = recruit.speciality.ToString();
            recruitCost.text = recruit.cost.ToString();

            recruitButton.onClick.RemoveAllListeners();

            recruitButton.onClick.AddListener(() =>
            {
                if (RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD] < recruit.cost)
                {
                   
                    return;
                }

                  
                SfxManager.PlaySfx("Click");
                recruitPanel.Recruit();
                m_currentIsland.m_availableRecruits.Remove(recruit);
                Destroy(entry);

            });

        }
    }
}

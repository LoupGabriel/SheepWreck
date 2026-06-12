using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiWorldMap : MonoBehaviour
{
    public TravelSystem m_travelSystem;


    [SerializeField] private GameObject m_travelPanel;
    [SerializeField] private TMP_Text m_destinationText;
    [SerializeField] private TMP_Text m_remainingTimeText;
    

    [SerializeField] private IslandInstance m_selectedIsland;

    [SerializeField] private Image m_shipToken;
    [SerializeField] private Image m_flagDestination;
   


    private void Start()
    {
        m_travelPanel.SetActive(false);
        m_shipToken.rectTransform.position = m_travelSystem.m_currentIsland.transform.position;
        TravelSystem.Instance.OnDestinationReach += SetBoatIcon;
    }
    private void Update()
    {
        if (!m_travelSystem.m_isTraveling) return;
        if (m_travelSystem.m_travelTime <= 0.1)
        {
            m_remainingTimeText.text = "At the island";
        }
        else
        {
            m_remainingTimeText.text = "Arrive in :" + m_travelSystem.m_travelTime.ToString();
        }
       
        
    }

    private void OnDestroy()
    {
        TravelSystem.Instance.OnDestinationReach -= SetBoatIcon;
    }

    public void ActiveWorldMap()
    {
        m_travelPanel.SetActive(!m_travelPanel.activeSelf);
    }

    public void SelectIsland(IslandInstance selectedIsland)
    {
        if (m_travelSystem.m_isTraveling) return;

        m_selectedIsland = selectedIsland;
        m_flagDestination.rectTransform.position = m_selectedIsland.transform.position;
        m_destinationText.text = "Destination :" + selectedIsland.m_islandName;


    }

    public void OnClickTravel()
    {
        if (m_selectedIsland == null) return;

       
        // to do Play sfx

        m_travelSystem.SetDestination(m_selectedIsland);
        m_travelPanel.SetActive(false);
    }

    private void SetBoatIcon(Vector3 newPos)
    {
        m_shipToken.rectTransform.position = newPos;
    }


}

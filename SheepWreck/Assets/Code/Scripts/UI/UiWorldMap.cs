using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiWorldMap : MonoBehaviour
{
    private TravelSystem m_travelSystem;

    [SerializeField] private GameObject m_travelPanel;
    [SerializeField] private TMP_Text m_destinationText;
    [SerializeField] private TMP_Text m_remainingTimeText;
    [SerializeField] private GameObject m_islandButton;
    
    [SerializeField] private TMP_Text m_energyCostText;
    [SerializeField] private IslandInstance m_selectedIsland;

    [SerializeField] private Image m_shipToken;
    [SerializeField] private Image m_flagDestination;
   
    private void Start()
    {
        m_travelSystem = TravelSystem.Instance;
        m_travelPanel.SetActive(false);
        m_shipToken.rectTransform.position = m_travelSystem.CurrentIsland.transform.position;
        TravelSystem.Instance.OnDestinationReach += SetBoatIcon;
    }
    private void Update()
    {
        if (!m_travelSystem.IsTraveling) return;
        if (m_travelSystem.TravelTime <= 0.1)
        {
            m_remainingTimeText.text = "At the island";
        }
        else
        {
            m_remainingTimeText.text = "Destination reach in :" +  Mathf.FloorToInt(m_travelSystem.TravelTime).ToString();
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
        if (m_travelSystem.IsTraveling) return;

        m_selectedIsland = selectedIsland;
        m_flagDestination.rectTransform.position = m_selectedIsland.transform.position;
        m_destinationText.text = "Destination :" + selectedIsland.IslandName;
        int energyCost = m_travelSystem.GetEnergyCost(selectedIsland);

        int currentEnergy = RessourceSystem.Instance.RessourceDictionary[ERessourceType.ENERGY];

        m_energyCostText.text = currentEnergy >= energyCost
            ? $"Energy cost : {energyCost}"
            : $"Not Enough Energy {energyCost}";

    }

    public void OnClickTravel()
    {
        if (m_selectedIsland == null) return;


        
        SfxManager.PlaySfx("Sail");
        m_travelSystem.SetDestination(m_selectedIsland);
        m_travelSystem.UpAnchor();
        GameManager.Instance.SetConstructionMode(false);
        UiPanelManager.Instance.CloseCurrentPanel();
        PauseController.IsPaused(false);
    }

    private void SetBoatIcon(Vector3 newPos)
    {
        m_shipToken.rectTransform.position = newPos;
    }

    private void SetIslandButtonVisible(Vector3 destination)
    {
        m_islandButton.gameObject.SetActive(true);
    }
    private void SetIslandButtonInvisible()
    {
        m_islandButton.gameObject.SetActive(false);
    }


}

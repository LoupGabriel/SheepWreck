using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiRoomConstructionPanel : MonoBehaviour
{
    [SerializeField] private RoomData m_roomData;


    [SerializeField] private Button m_buyButton;

    [SerializeField] private TMP_Text m_roomName;
    [SerializeField] private TMP_Text m_description;
    [SerializeField] private Image m_roomIcon;


    [SerializeField] private TMP_Text m_buildCost;
    [SerializeField] private TMP_Text m_ressourceProduce;
    [SerializeField] private TMP_Text m_upkeepCost;

    [SerializeField] private GameObject m_roomPrefabs;


    private void Start()
    {
        m_roomName.text = m_roomData.m_roomName;
        m_description.text = m_roomData.m_description;
        m_roomIcon.sprite = m_roomData.m_roomIcon;
        m_buildCost.text = m_roomData.m_buildCost.ToString();
        m_upkeepCost.text = "Upkeep :" + m_roomData.m_upkeepCost.ToString();
        m_ressourceProduce.text = "Produce :" +m_roomData.m_ressourceProduced.ToString();
    }


    public void OnClickBuy()
    {
        if (RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD] < m_roomData.m_buildCost) {
            
            GameManager.Instance.SetConstructionMode(false);

            PauseController.IsPaused(false);
            SfxManager.PlaySfx("Error");
            return; 
        
        
        }
        //UiPanelManager.Instance.CloseCurrentPanel();
        UiPanelManager.Instance.closeConstructionPanel();
        //pay the gold amount
        RessourceSystem.Instance.GainRessource(m_roomData.m_buildCost * -1,ERessourceType.GOLD);

        //store new room prefab
        ShipSystem.Instance.SetRoomPrefab(m_roomPrefabs);
        PauseController.IsPaused(false);

    }
}



using System.Collections.Generic;
using TMPro;
using UnityEngine;




public class UIRessources : MonoBehaviour
{

    [SerializeField] private TMP_Text m_currentGoldText;
    [SerializeField] private TMP_Text m_currentFoodText;
    [SerializeField] private TMP_Text m_currentWaterText;
    [SerializeField] private TMP_Text m_currentEnergyText;
    [SerializeField] private TMP_Text m_currentMoraleText;
    [SerializeField] private TMP_Text m_currentSheepText;
    [SerializeField] private TMP_Text m_currentResearchText;

    [SerializeField] private TMP_Text m_currentGoldPopup;
    [SerializeField] private TMP_Text m_currentFoodPopup;
    [SerializeField] private TMP_Text m_currentWaterPopup;
    [SerializeField] private TMP_Text m_currentEnergyPopup;
    [SerializeField] private TMP_Text m_currentMoralePopup;
    [SerializeField] private TMP_Text m_currentSheepPopup;
    [SerializeField] private TMP_Text m_currentResearchPopup;

    [SerializeField] CrewManager m_crewManager;

    private Dictionary<ERessourceType, TMP_Text> m_ressourceText = new Dictionary<ERessourceType, TMP_Text>();
    private Dictionary<ERessourceType, TMP_Text> m_ressourcePopup = new Dictionary<ERessourceType, TMP_Text>();
    private Dictionary<ERessourceType, float> m_ressourceOldAmount = new Dictionary<ERessourceType, float>();
    private float m_oldValue;
    private void OnEnable()
    {
        RessourceSystem.Instance.OnRessourceChange += NotifyRessourceChange;
        ShipSystem.Instance.OnRoomSwitchRessource += NotifyRessourceChange;

    }

    private void OnDisable()
    {
        RessourceSystem.Instance.OnRessourceChange -= NotifyRessourceChange;
        ShipSystem.Instance.OnRoomSwitchRessource -= NotifyRessourceChange;
    }
    private void Start()
    {
        m_ressourceText.Add(ERessourceType.GOLD, m_currentGoldText);
        m_ressourceText.Add(ERessourceType.FOOD, m_currentFoodText);
        m_ressourceText.Add(ERessourceType.WATER, m_currentWaterText);
        m_ressourceText.Add(ERessourceType.ENERGY, m_currentEnergyText);
        m_ressourceText.Add(ERessourceType.MORALE, m_currentMoraleText);
        m_ressourceText.Add(ERessourceType.RESEARCH, m_currentResearchText);

        m_ressourcePopup.Add(ERessourceType.GOLD, m_currentGoldPopup);
        m_ressourcePopup.Add(ERessourceType.FOOD, m_currentFoodPopup);
        m_ressourcePopup.Add(ERessourceType.WATER, m_currentWaterPopup);
        m_ressourcePopup.Add(ERessourceType.ENERGY, m_currentEnergyPopup);
        m_ressourcePopup.Add(ERessourceType.MORALE, m_currentMoralePopup);
        m_ressourcePopup.Add(ERessourceType.RESEARCH, m_currentResearchPopup);

        m_ressourceOldAmount.Add(ERessourceType.GOLD, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD]);
        m_ressourceOldAmount.Add(ERessourceType.FOOD, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.FOOD]);
        m_ressourceOldAmount.Add(ERessourceType.WATER, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.WATER]);
        m_ressourceOldAmount.Add(ERessourceType.ENERGY, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY]);
        m_ressourceOldAmount.Add(ERessourceType.MORALE, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.MORALE]);
        m_ressourceOldAmount.Add(ERessourceType.RESEARCH, RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.RESEARCH]);


        m_currentGoldText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.GOLD].ToString() + "/" + RessourceSystem.Instance.GetRessource(ERessourceType.GOLD);
        m_currentFoodText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.FOOD].ToString() + "/" + RessourceSystem.Instance.GetRessource(ERessourceType.FOOD);
        m_currentWaterText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.WATER].ToString() + "/" + RessourceSystem.Instance.GetRessource(ERessourceType.WATER);
        m_currentEnergyText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY].ToString() + "/" + RessourceSystem.Instance.GetRessource(ERessourceType.ENERGY);
        m_currentResearchText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.RESEARCH].ToString() + "/" + RessourceSystem.Instance.GetRessource(ERessourceType.RESEARCH);


        m_ressourcePopup[ERessourceType.GOLD].gameObject.SetActive(false);
        m_ressourcePopup[ERessourceType.FOOD].gameObject.SetActive(false);
        m_ressourcePopup[ERessourceType.WATER].gameObject.SetActive(false);
        m_ressourcePopup[ERessourceType.ENERGY].gameObject.SetActive(false);
        m_ressourcePopup[ERessourceType.MORALE].gameObject.SetActive(false);
        m_ressourcePopup[ERessourceType.RESEARCH].gameObject.SetActive(false);






        
    }


    /// <summary>
    ///  Notify the ressource ui to change a specific ressource
    /// </summary>
    /// <param name="ressourceType"> Take the ressource type and return the ui text associate with</param>
    public void NotifyRessourceChange(ERessourceType ressourceType)
    {

        if (m_ressourceText.TryGetValue(ressourceType, out TMP_Text text))
        {

            text.text = RessourceSystem.Instance.m_ressourceDictionary[ressourceType].ToString() + "/" + RessourceSystem.Instance.GetRessource(ressourceType);
        }
        if (m_ressourcePopup.TryGetValue(ressourceType, out TMP_Text popup))
        {
            m_ressourcePopup[ressourceType].gameObject.SetActive(true);
            float difference = (RessourceSystem.Instance.m_ressourceDictionary[ressourceType] - m_ressourceOldAmount[ressourceType]);
            if( difference > 0 )
            {
                popup.color = Color.green;
            }
            else
            {
                popup.color = Color.red;

            }
            popup.text = difference.ToString();
            popup.gameObject.SetActive(true);
            m_ressourceOldAmount[ressourceType] = RessourceSystem.Instance.m_ressourceDictionary[ressourceType];
        }
    }

    private void Update()
    {
        m_currentMoraleText.text = GetAverageMorale().ToString();
        m_currentSheepText.text = CrewManager.Instance.m_currentSheepOnBoard.Count.ToString();
    }

    /// <summary>
    /// Return the average Morale off all sheep in crewManager
    /// </summary>
    public int GetAverageMorale()
    {


        int total = 0;
        for (int i = 0; i < m_crewManager.m_currentSheepOnBoard.Count; i++)
        {

            total += m_crewManager.m_currentSheepOnBoard[i].m_currentMorale;

        }

        if (m_crewManager.m_currentSheepOnBoard.Count != 0)
        {
            return total / m_crewManager.m_currentSheepOnBoard.Count;
        }
        else { return 0; }


    }

}

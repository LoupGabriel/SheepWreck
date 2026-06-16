using Mono.Cecil;
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
    [SerializeField] CrewManager m_crewManager;

    private Dictionary<ERessourceType, TMP_Text> m_ressourceText = new Dictionary<ERessourceType, TMP_Text>();

    private void OnEnable()
    {
        RessourceSystem.Instance.OnRessourceChange += NotifyRessourceChange;
        
    }

    private void OnDisable()
    {
        RessourceSystem.Instance.OnRessourceChange -= NotifyRessourceChange;
    }
    private void Start()
    {
        m_ressourceText.Add(ERessourceType.GOLD, m_currentGoldText);
        m_ressourceText.Add(ERessourceType.FOOD, m_currentFoodText);
        m_ressourceText.Add(ERessourceType.WATER, m_currentWaterText);
        m_ressourceText.Add(ERessourceType.ENERGY, m_currentEnergyText);
        m_ressourceText.Add(ERessourceType.MORALE, m_currentMoraleText);

        m_currentEnergyText.text = RessourceSystem.Instance.m_ressourceDictionary[ERessourceType.ENERGY].ToString();


    }


    /// <summary>
    ///  Notify the ressource ui to change a specific ressource
    /// </summary>
    /// <param name="ressourceType"> Take the ressource type and return the ui text associate with</param>
    public void NotifyRessourceChange(ERessourceType ressourceType)
    {
        if (m_ressourceText.TryGetValue(ressourceType, out TMP_Text text))
               {

            text.text = RessourceSystem.Instance.m_ressourceDictionary[ressourceType].ToString();
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

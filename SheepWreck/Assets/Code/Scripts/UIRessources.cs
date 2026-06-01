using UnityEngine;
using TMPro;
public class UIRessources : MonoBehaviour
{
    [SerializeField] private TMP_Text m_currentFoodText;
    [SerializeField] private TMP_Text m_currentWaterText;
    [SerializeField] private TMP_Text m_currentMoraleText;
    [SerializeField] CrewManager m_crewManager;


    public void NotifyFoodChange(int food)
    {
        m_currentFoodText.text = food.ToString();
    }
    public void NotifyWaterChange(int water)
    {

        m_currentWaterText.text = water.ToString();
    }

    private void Update()
    {
        m_currentMoraleText.text = GetAverageMorale().ToString();
    }


    public int GetAverageMorale()
    {


        int total = 0;
        for(int i = 0; i < m_crewManager.m_currentSheepOnBoard.Count; i++)
        {

            total += m_crewManager.m_currentSheepOnBoard[i].m_currentMorale;

        }

        return total / m_crewManager.m_currentSheepOnBoard.Count;


    }
    
}

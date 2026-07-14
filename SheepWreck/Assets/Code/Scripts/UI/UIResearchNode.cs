using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchNode : MonoBehaviour
{
    [SerializeField] private ResearchNodeSo m_research;
    [SerializeField] private TMP_Text m_cost;
    [SerializeField] private Image m_status;
    [SerializeField] private Sprite m_blocked;
    [SerializeField] private Sprite m_unlock;
    [SerializeField] private Sprite m_completed;
    [SerializeField] private Sprite m_inProgress;

    private Button m_button;


    private void Start()
    {
        m_button = GetComponent<Button>();
        string cost = m_research.researchCost.ToString();

        if (m_research.researchCost == 0)
        {
            m_cost.text = "Free";
        }
        else
        {

            m_cost.text = m_research.researchCost.ToString();
        }

        m_status.sprite = GetStatus();
    }

    private void Update()
    {
        m_status.sprite = GetStatus();
    }
    public void OnClick()
    {

        if (m_research == null)
            return;

        ResearchSystem.Instance.StartResearch(m_research);
    }

    private Sprite GetStatus()
    {

        ResearchProgress progress = ResearchSystem.Instance.FindProgress(m_research);

        if(progress != null)
        {
            if (ResearchSystem.Instance.IsResearchUnlock(m_research) && !progress.isCompleted && progress.currentProgress <=0)
            {
                return m_unlock;
            }
            else if (!ResearchSystem.Instance.IsResearchUnlock(m_research) && !progress.isCompleted && progress.currentProgress <= 0)
            {
                return m_blocked;
            }
            else if(progress.isCompleted && progress.isUnlocked)
            {
                return m_completed;
            }
            else if(ResearchSystem.Instance.IsResearchUnlock(m_research) && !progress.isCompleted && progress.currentProgress > 0)
            {
                return m_inProgress;
            }
           
         


           

        }
        return null;
    }



}

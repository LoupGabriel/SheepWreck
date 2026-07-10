using UnityEngine;

public class UIResearchNode : MonoBehaviour
{
    [SerializeField] private ResearchNodeSo m_research;


    public void OnClick()
    {

        if (m_research == null)
            return;

        ResearchSystem.Instance.StartResearch(m_research);
    }
    
}
